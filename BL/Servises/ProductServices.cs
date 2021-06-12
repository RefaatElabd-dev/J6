using J6.DAL.Database;
using J6.DAL.Entities;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Authorization;
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
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

<<<<<<< HEAD

=======
        [Authorize]
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
        public async Task AssignToViewsAsync(int UserId, int ProductId)
        {
            await _context.Views.AddAsync(new View { CustomerId = UserId, ProductId = ProductId });
            await _context.SaveChangesAsync();
        }

<<<<<<< HEAD
        public async Task<List<Product>> GetRandomProductsAsync()
        {
            Random rand = new Random();
            int toSkip = rand.Next(0, _context.Products.Count());

            List<Product> products = await _context.Products.Skip(toSkip).Take(17).ToListAsync();
=======
        [Authorize]
        public async Task<bool> DeleteViewsAsync(int UserId, int ProductId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if(user != null)
            {
                View ViewRow = await _context.Views.FirstOrDefaultAsync(v => v.CustomerId == UserId && v.ProductId == ProductId);
                if(ViewRow != null)
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
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
            return products;
        }

        public async Task<List<Product>> GetRecomendedProductsAsync(int UserId)
<<<<<<< HEAD
        {
=======
        {   
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
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
<<<<<<< HEAD
                Random rand = new Random();
                int toSkip = rand.Next(0, _context.Products.Count());

                List<Product> Restproducts = await _context.Products.Skip(toSkip).Take(17 - products.Count()).ToListAsync();
=======
                //Random rand = new Random();
                //int toSkip = rand.Next(0, _context.Products.Count());

                List<Product> Restproducts = await _context.Products.OrderBy(p => Guid.NewGuid()).Take(17 - products.Count()).ToListAsync();
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b

                products.AddRange(Restproducts);
            }

            return products;
        }
<<<<<<< HEAD
=======

        [Authorize]
        public async Task<List<Product>> GetViewsByDateAsync(int UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if(user != null)
            {
                //var date = await _context.Views.Select(v => v.CreationDate).ToListAsync();
                List<Product> products = await _context.Products.Where(p=> p.Views.Any( v=>v.CustomerId==UserId && v.ProductId == p.ProductId)).OrderByDescending(p => p.Views.OrderBy(v=>p.ProductId).First().CreationDate).Take(50).ToListAsync();
                return products;
            }

            return null;
        }
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
    }
}
