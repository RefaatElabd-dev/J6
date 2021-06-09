using AutoMapper;
using AutoMapper.QueryableExtensions;
using J6.DAL.Database;
using J6.Helper;
using J6.Interfaces;
using J6.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

using J6.DAL.Entities;
using J6.BL.Helper;

namespace J6.BL.Repositry
{
    public class ProductRepositry : IProductRepository
    {


        private readonly DbContainer _context;
        private readonly IMapper _mapper;



        //because repo have relate with db we will inject db contect in constructor
        public ProductRepositry(DbContainer context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(ProductParams productParams)
        {
            return await _context.Products.Where
                (x => (productParams.Model == null || x.BrandName.ToLower().
                Contains(productParams.Model.ToLower())) || (productParams.Color == null || x.Color.ToLower().Contains(productParams.Color))).ToListAsync();
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> GetProductByNameAsync(string productname)
        {
            return await _context.Products
                .Include(p => p.Subcategory).
                SingleOrDefaultAsync(x => x.ProductName == productname);
        }

        public async Task<bool> SaveAllAsync() => await _context.SaveChangesAsync() > 0;

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }


        /// <summary>
        /// By Auto Mapper
        /// </summary>
        /// <returns></returns>



        public async Task<PageList<ProductDto>> GetProdsAsync(ProductParams productParams)
        {

            //var query = _context.Products
            //   .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            //   .AsNoTracking()
            //   .AsQueryable();



            var query = _context.Products.AsQueryable();

            query = query.Where(x => (productParams.Model == null || x.Model.ToLower()
            .Contains(productParams.Model.ToLower()))|| (productParams.Size == null || x.Size.ToLower()
            .Contains(productParams.Size.ToLower()))||(productParams.Color==null||x.Color.ToLower().Contains(productParams.Color)));


            return await PageList<ProductDto>.CreateAsync(query.ProjectTo<ProductDto>
                (_mapper.ConfigurationProvider).AsNoTracking()
                , productParams.PageNumber, productParams.PageSize);

        }

        public async Task<ProductDto> GetProdByIdAsync(int id)
        {
            return await _context.Products
            .Where(x => x.ProductId == id)
          .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
          .SingleOrDefaultAsync();
        }

        public async Task<ProductDto> GetProdByNameAsync(string productname)
        {
            return await _context.Products
               .Where(x => x.ProductName == productname)
               .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
               .SingleOrDefaultAsync();
        }

    }
}