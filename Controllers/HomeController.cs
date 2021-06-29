using J6.BL.Servises;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace J6.Controllers
{
    public class HomeController : Controller
    {

        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAdminStatisticsService _adminStatistics;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(RoleManager<AppRole> roleManager, IAdminStatisticsService adminStatistics, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _adminStatistics = adminStatistics;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            ViewData["CustomersNumber"] = await _adminStatistics.GetCustomersNumber();
            ViewData["SellersNumber"] = await _adminStatistics.GetSellersNumber();
            ViewData["ProductsNumber"] = await _adminStatistics.GetProductsNumber();
            ViewData["SavedProductsNumber"] = await _adminStatistics.GetSavedProductsNumber();
            ViewData["SolidItemsNumber"] = await _adminStatistics.GetSolidItemsNumber();
            ViewData["ViewedProductsNumber"] = await _adminStatistics.GetViewedProductsNumber();
            ViewData["SellingRate"] = await _adminStatistics.GetrateOfSViewedProducts();
            return View();
        }


        public IActionResult ColorCards()
        {
            return View();
        }
    }
}
