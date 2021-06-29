using J6.DAL.Database;
using J6.DAL.Entities;
using J6.Models;
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

            products = await _context.Products.Where(p => p.Id == ProductId).Include(p => p.Promotion).Take(17).ToListAsync();

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

        public async Task<Review> AddReviewToProductAsync(ReviewModel reviewModel)
        {
            IEnumerable<int> CustomerOrders = await _context.Orders.Where(O => O.CustimerId == reviewModel.CustomerId && O.Status == OrderStatus.Done).Select(O => O.Id).ToListAsync();
            if (CustomerOrders == null) return null;
            int ProductID = await _context.ProdOrders.Where(PO => CustomerOrders.Contains(PO.OrderId) && PO.ProductId == reviewModel.ProductId)
                                                            .Select(PO => PO.ProductId).FirstOrDefaultAsync();

            if (ProductID == 0) return null;
            Review review = new Review()
            {
                ProductId = reviewModel.ProductId,
                CustomerId = reviewModel.CustomerId,
                Comment = reviewModel.Comment,
                Rating = reviewModel.Rating
            };
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            await EditProductRatingAsync(reviewModel.ProductId);
            return await _context.Reviews.OrderByDescending(R => R.CreationTime).FirstOrDefaultAsync();
        }

        public async Task<Review> EditReviewAsync(ReviewModel reviewModel)
        {
            Review review = await _context.Reviews.Where(R => R.ProductId == reviewModel.ProductId && R.CustomerId == reviewModel.CustomerId).FirstOrDefaultAsync();
            if (review == null) return null;
            review.Rating = reviewModel.Rating;
            review.Comment = reviewModel.Comment;
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            await EditProductRatingAsync(reviewModel.ProductId);
            return review;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsOfProductAsync(int productId)
        {
            return await _context.Reviews.Where(R => R.ProductId == productId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllReviewsOfCustomerAsync(int customerId)
        {
            return await _context.Reviews.Where(R => R.CustomerId == customerId).ToListAsync();
        }
        public async Task<double> EditProductRatingAsync(int productId)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            IEnumerable<double> ReviewsRating = await _context.Reviews
                                                              .Where(R => R.ProductId == productId)
                                                              .Select(R => R.Rating)
                                                              .ToListAsync();
            if (ReviewsRating.Count() != 0)
            {
                double sum = ReviewsRating.Average();
                sum = ((int)(sum * 10))/10.0;
                product.Rating = sum;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            return product.Rating;
        }

        public async Task<double> GetProductRatingAsync(int productId)
        {
            return await EditProductRatingAsync(productId);
        }
    }
}