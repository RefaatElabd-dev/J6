using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public async Task AssignToViewsAsync(int UserId, int ProductId)
        {
            await _context.Views.AddAsync(new View { CustomerId = UserId, ProductId = ProductId });
            await _context.SaveChangesAsync();
        }

        [Authorize]
        public async Task<bool> DeleteViewsAsync(int UserId, int ProductId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user != null)
            {
                View ViewRow = await _context.Views.FirstOrDefaultAsync(v => v.CustomerId == UserId && v.ProductId == ProductId);
                if (ViewRow != null)
                {
                    _context.Views.Remove(ViewRow);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<List<Product>> GetRandomProductsAsync()
        {
            //Random rand = new Random();
            //int toSkip = rand.Next(0, _context.Products.Count());
            List<Product> products = await _context.Products.OrderBy(p => Guid.NewGuid()).Take(17).ToListAsync();

            //List<Product> products = await _context.Products.Skip(toSkip).Take(17).ToListAsync();
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

            products = await _context.Products.Where(p => p.Id == ProductId).Include(p => p.ProductImages).Include(p => p.Promotion).Take(17).ToListAsync();

            if (products.Count() < 17)
            {
                //Random rand = new Random();
                //int toSkip = rand.Next(0, _context.Products.Count());

                List<Product> Restproducts = await _context.Products.OrderBy(p => Guid.NewGuid()).Take(17 - products.Count()).ToListAsync();

                products.AddRange(Restproducts);
            }

            return products;
        }

        [Authorize]
        public async Task<List<Product>> GetViewsByDateAsync(int UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user != null)
            {
                //var date = await _context.Views.Select(v => v.CreationDate).ToListAsync();
                List<Product> products = await _context.Products.Where(p => p.Views.Any(v => v.CustomerId == UserId && v.ProductId == p.Id)).OrderByDescending(p => p.Views.OrderBy(v => p.Id).First().CreationDate).Take(50).ToListAsync();
                return products;
            }

            return null;
        }
    }
}