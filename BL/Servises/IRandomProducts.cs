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
<<<<<<< HEAD

=======
        public Task<bool> DeleteViewsAsync(int UserId, int ProductId);
        public Task<List<Product>> GetViewsByDateAsync(int UserId);
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
        public Task<List<Product>> GetRandomProductsAsync();

        public Task<List<Product>> GetRecomendedProductsAsync(int UserId);
    }
}
