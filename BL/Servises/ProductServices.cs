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
    public class ProductServices : IRandomProducts
    {
        private readonly DbContainer _context;
        private readonly UserManager<AppUser> _userManager;

        public ProductServices(DbContainer context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task AssignToViewsAsync(int UserId, int ProductId)
        {
            await _context.Views.AddAsync(new View { CustomerId = UserId, ProductId = ProductId });
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetRandomProductsAsync()
        {
            Random rand = new Random();
            int toSkip = rand.Next(0, _context.Products.Count());

            List<Product> products = await _context.Products.Skip(toSkip).Take(17).ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetRecomendedProductsAsync(int UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            int ProductId = await _context.Views.Where(v => v.CustomerId == UserId).Select(v => v.ProductId).FirstOrDefaultAsync();
            
            List<Product> products = null;
            if (user == null || ProductId == 0)
            {
                Random rand = new Random();
                int toSkip = rand.Next(0, _context.Products.Count());

                products = await _context.Products.Skip(toSkip).Take(17).ToListAsync();
                return products;
            }

            products = await _context.Products.Where(p => p.ProductId == ProductId).Include(p => p.ProductImages).Include(p => p.Promotion).Take(17).ToListAsync();
            
            if(products.Count() < 17)
            {
                Random rand = new Random();
                int toSkip = rand.Next(0, _context.Products.Count());

                List<Product> Restproducts = await _context.Products.Skip(toSkip).Take(17 - products.Count()).ToListAsync();

                products.AddRange(Restproducts);
            }

            return products;
        }
    }
}
