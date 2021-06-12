using J6.DAL.Entities;
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
    }
}
