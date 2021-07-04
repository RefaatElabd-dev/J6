using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using J6.BL.Helper;
using J6.Models;
using J6.DAL.Entities;
using J6.BL.Servises;
using Microsoft.EntityFrameworkCore;

namespace J6.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly ITokenServices _tokenService;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , ILogger<AccountController> logger, ITokenServices tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            _tokenService = tokenService;
        }
        public IActionResult Registration()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Registration(RegistrationVM model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        var user = new AppUser()
        //        {
        //            UserName = model.Email.ToLower() ,
        //            Email = model.Email.ToLower()
        //        };

        //        var result = await userManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Login");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }
        //        }


        //    }

        //    return View(model);
        //}




        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == model.Email.ToLower());
                if (user == null) return Unauthorized("This UserName is not Exist");
                

                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RemomberMe, false);
                 
                if (result.Succeeded)
                {

                    if (await userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (await userManager.IsInRoleAsync(user, "Seller"))
                    {
                        if (!user.IsActive)
                        {
                            ModelState.AddModelError("", "You are Blocked OR Not Submitted Yet");
                        }
                        else
                        {
                            return RedirectToAction("GetSellerProduct", "Seller", new { id = user.Id }, "Seller");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid UserName Or Password Attempt, Or You Is In Wrong Place");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid UserName Or Password Attempt");
                }
            }
            
            return View(model);
        }

        [HttpPost]

        [HttpPost]
        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }


        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                    MailHelper.sendMail("Password Reset Link", passwordResetLink);

                    logger.Log(LogLevel.Warning, passwordResetLink);

                    return RedirectToAction("ConfirmForgetPassword");
                }

                return RedirectToAction("ConfirmForgetPassword");

            }

            return View(model);
        }



        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        public IActionResult ResetPassword(string Email , string Token)
        {
            if(Email == null || Token == null)
            {
                ModelState.AddModelError("", "Invalid Data");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ConfirmResetPassword");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }

                return RedirectToAction("ConfirmResetPassword");
            }

            return View(model);
        }


        public IActionResult ConfirmResetPassword()
        {
            return View();
        }
    }
}
