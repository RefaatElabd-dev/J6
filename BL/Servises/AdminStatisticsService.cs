using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.BL.Servises
{
    public class AdminStatisticsService : IAdminStatisticsService
    {
        private readonly DbContainer _context;
        private readonly UserManager<AppUser> _userManager;

        public AdminStatisticsService(DbContainer context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<int> GetCustomersNumber()
        {
            ICollection<AppUser> Customers = await _userManager.GetUsersInRoleAsync("Customer");
            return Customers.Count();
        }

        public int GetProductsNumber()
        {
            return _context.Products.Count();
        }

        public  double GetrateOfSViewedProducts()
        {
            return GetSolidItemsNumber()/(double)GetProductsNumber();
        }

        public int GetSavedProductsNumber()
        {
           return _context.ProductsBag.Count();
        }

        public async Task<int> GetSellersNumber()
        {
            ICollection<AppUser> Sellers = await _userManager.GetUsersInRoleAsync("Seller");
            return Sellers.Count();
        }

        public int GetSolidItemsNumber()
        {
            return _context.Products.Where(p => p.SoldQuantities >= 0).Count();
        }

        public int GetViewedProductsNumber()
        {
            return _context.Views.Count();
        }
    }
}
