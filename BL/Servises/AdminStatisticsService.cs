using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> GetProductsNumber()
        {
            var products = await _context.Products.ToListAsync();
            return products.Count();
        }

        public async Task<double> GetrateOfSViewedProducts()
        {
            int PNumbers = await GetProductsNumber();
            int SPNumbers =await GetSolidItemsNumber();
            return SPNumbers/(double)PNumbers;
        }

        public async Task<int> GetSavedProductsNumber()
        {
            var Bags = await _context.Products.ToListAsync();
           return Bags.Count();
        }

        public async Task<int> GetSellersNumber()
        {
            ICollection<AppUser> Sellers = await _userManager.GetUsersInRoleAsync("Seller");
            return Sellers.Count();
        }

        public async Task<int> GetSolidItemsNumber()
        {
            var SolidProducts = await _context.Products.Where(p => p.SoldQuantities >= 0).ToListAsync();
            return SolidProducts.Count();
        }

        public async Task<int> GetViewedProductsNumber()
        {
            var v = await _context.Views.ToListAsync();
            return v.Count();
        }
    }
}
