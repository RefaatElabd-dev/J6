using J6.BL.Servises;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Controllers
{
    public class HomeController : Controller
    {
<<<<<<< HEAD
        public IActionResult Index(string role)
        {
            
            return View(role);
=======
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAdminStatisticsService _adminStatistics;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(RoleManager<AppRole> roleManager, IAdminStatisticsService adminStatistics, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _adminStatistics = adminStatistics;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CustomersNumber"] = await _adminStatistics.GetCustomersNumber();
            ViewData["SellersNumber"] = await _adminStatistics.GetSellersNumber();
            ViewData["ProductsNumber"] = await _adminStatistics.GetProductsNumber();
            ViewData["SavedProductsNumber"] = await _adminStatistics.GetSavedProductsNumber();
            ViewData["SolidItemsNumber"] = await _adminStatistics.GetSolidItemsNumber();
            ViewData["ViewedProductsNumber"] = await _adminStatistics.GetViewedProductsNumber();
            ViewData["SellingRate"] = await _adminStatistics.GetrateOfSViewedProducts();
            var data = _roleManager.Roles;
            return View(data);
>>>>>>> 7a32740fa54f1528f3c66eef100340d4211c0486
        }


        public IActionResult ColorCards()
        {
            return View();
        }
    }
}
