using J6.BL.Helper;
using J6.DAL.Entities;
using J6.Helper;
using J6.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace J6.Interfaces
{
    public interface IProductRepository
    {
        void Update(Product product);

        Task<bool> SaveAllAsync();

        Task<IEnumerable<Product>> GetProductsAsync(ProductParams productParams);
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProductByNameAsync(string productname);



        //shaban
        Task<PageList<ProductDto>> GetProdsAsync(ProductParams productParams);
        Task <ProductDto>GetProdByIdAsync(int id);
        Task <ProductDto>GetProdByNameAsync(string productname);


    }
}
