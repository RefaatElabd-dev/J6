using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using J6.Models;
using J6.BL.Servises;
using J6.DAL.Entities;

namespace J6.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAdminStatisticsService _adminStatistics;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(RoleManager<AppRole> roleManager, IAdminStatisticsService adminStatistics, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _adminStatistics = adminStatistics;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["CustomersNumber"] = _adminStatistics.GetCustomersNumber();
            ViewData["SellersNumber"] = _adminStatistics.GetSellersNumber();
            ViewData["ProductsNumber"] = _adminStatistics.GetProductsNumber();
            ViewData["SavedProductsNumber"] = _adminStatistics.GetSavedProductsNumber();
            ViewData["SolidItemsNumber"] = _adminStatistics.GetSolidItemsNumber();
            ViewData["ViewedProductsNumber"] = _adminStatistics.GetViewedProductsNumber();
            ViewData["SellingRate"] = _adminStatistics.GetrateOfSViewedProducts();
            var data = _roleManager.Roles;
            return View(data);
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleVM model)
        {
            if (ModelState.IsValid)
            {

                var role = new AppRole()
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("CreateRole");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }


        public async Task<IActionResult> EditRole(string Id)
        {
            var role =  await _roleManager.FindByIdAsync(Id);


            var data = new EditRoleVM()
            {
                Id = role.Id.ToString(),
                RoleName = role.Name
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleVM model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);

                role.Name = model.RoleName;

                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);


            var data = new DeleteRole()
            {
                Id = role.Id.ToString(),
                RoleName = role.Name
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteRole model)
        {

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);


                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        //#######################Admin Statistics###############


        [HttpGet]
        public async Task<IActionResult> GetAllInActiveSellers()
        {
            var sellers = await _userManager.GetUsersInRoleAsync("Seller");
            ICollection<AppUser> notActiveSellers = sellers.Where(S => S.IsActive == false).ToList();
            return View(notActiveSellers);
        }

        [HttpPost]
        public async Task ApproveSeller(int SellerId)
        {
            var seller = await _userManager.FindByIdAsync(SellerId.ToString());
            seller.IsActive = true;
            await _userManager.UpdateAsync(seller);
        }


    }
}
