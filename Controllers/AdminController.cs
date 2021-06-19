using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using J6.Models;
using J6.BL.Servises;

namespace J6.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _userManager;
        private readonly IAdminStatisticsService _adminStatistics;

        public AdminController(RoleManager<IdentityRole> userManager, IAdminStatisticsService adminStatistics)
        {
            _userManager = userManager;
            _adminStatistics = adminStatistics;
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
            var data = _userManager.Roles;
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

                var role = new IdentityRole()
                {
                    Name = model.RoleName
                };

                var result = await _userManager.CreateAsync(role);

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
            var role =  await _userManager.FindByIdAsync(Id);


            var data = new EditRoleVM()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return View(data);
        }



        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleVM model)
        {

            if (ModelState.IsValid)
            {
                var role = await _userManager.FindByIdAsync(model.Id);

                role.Name = model.RoleName;

                var result = await _userManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }


            }


        

            return View(model);
        }



        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await _userManager.FindByIdAsync(Id);


            var data = new DeleteRole()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return View(data);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteRole model)
        {

            if (ModelState.IsValid)
            {
                var role = await _userManager.FindByIdAsync(model.Id);


                var result = await _userManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }


            }

            return View(model);
        }

        //#######################Admin Statistics###############



    }
}
