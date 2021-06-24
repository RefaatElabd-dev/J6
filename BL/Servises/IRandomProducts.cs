using J6.DAL.Entities;
using J6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.BL.Servises
{
    public interface IRandomProducts
    {
        public Task AssignToViewsAsync(int UserId, int ProductId);
        public Task<bool> DeleteViewsAsync(int UserId, int ProductId);
        public Task<List<Product>> GetViewsByDateAsync(int UserId);
        public Task<List<Product>> GetRandomProductsAsync();

        public Task<List<Product>> GetRecomendedProductsAsync(int UserId);

        public Task<Review> AddReviewToProductAsync(ReviewModel reviewModel);
        public Task<Review> EditReviewAsync(ReviewModel reviewModel);
        public Task<IEnumerable<Review>> GetAllReviewsOfProductAsync(int productId);
        public Task<IEnumerable<Review>> GetAllReviewsOfCustomerAsync(int customerId);
    }
}