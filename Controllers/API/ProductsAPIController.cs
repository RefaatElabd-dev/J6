using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using J6.BL.Helper;
using J6.BL.Servises;
using J6.DAL.Database;
using J6.DAL.Entities;
using J6.Extentions;
using J6.Helper;
using J6.Interfaces;
using J6.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace J6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPiController : ControllerBase
    {
        private readonly DbContainer _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRandomProducts _products;

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductsAPiController(DbContainer context, UserManager<AppUser> userManager, IRandomProducts products, IProductRepository productRepository, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _products = products;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          var products= await _context.Products.ToListAsync();
           return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            // var product = await _context.Products.FindAsync(id);

            var product = await _context.Products.Include(a => a.Promotion).Include(a => a.ShippingDetail).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).FirstOrDefaultAsync(q => q.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        //high selling product
        // /highselling
        [HttpGet]
        [Route("~/highselling")]
        public async Task<ActionResult<IEnumerable<Product>>> topSellingProduct()
        {
            List<Product> highproducts = new List<Product>();
            var allproduct = await _context.Products.OrderByDescending(p => p.SoldQuantities).Include(a => a.ProductImages).Include(a => a.Promotion).Include(c => c.Reviews).Include(w => w.ShippingDetail).Include(q => q.ProdCarts)/*.Include(o=>o.ProductBrands)*/.Include(p => p.ProdOrders).ToListAsync();
            for (int i = 0; i < 10; i++)
            {


                highproducts.Add(allproduct[i]);


            }

            return highproducts;

        }


        //getallbycreatedat
        //sortbylastadd
        //    allproduct
        [HttpGet]
        [Route("~/allproduct")]
        public async Task<ActionResult<IEnumerable<Product>>> allproducts()
        {

            var allproduct = await _context.Products.OrderByDescending(p => p.CreatedAt).Include(a => a.ProductImages).Include(a => a.Promotion).Include(c => c.Reviews).Include(w => w.ShippingDetail).Include(q => q.ProdCarts)/*.Include(o => o.ProductBrands)*/.Include(p => p.ProdOrders).ToListAsync();

            return allproduct;

        }



        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }


        //############################################################################################################

        [HttpPost]
        [Route("SetView")]
        public async Task<IActionResult> SetView(int UserId, int ProductId)
        {
            await _products.AssignToViewsAsync(UserId, ProductId);
            return NoContent();
        }

        [HttpGet]
        [Route("GetRandomProducts")]
        public async Task<ActionResult<List<Product>>> GetRandomProducts()
        {
            return Ok(await _products.GetRandomProductsAsync());
        }

        [HttpGet("{CustomerId}")]
        [Route("GetRecomendedProducts/{CustomerId}")]
        public async Task<ActionResult<List<Product>>> GetRecomendedProducts(int CustomerId)
        {
            return Ok(await _products.GetRecomendedProductsAsync(CustomerId));
        }


        //----------------------------------------------------------------------------------------




        #region FilterProducts
        //api/ProductsAPi/FilterProducts
        //api/ProductsAPi/FilterProducts?size=sm&color=green&model=coton
        [HttpGet]
        [Route("FilterProducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> FilterProducts
            ([FromQuery] ProductParams productParams)
        {

            var products = await _productRepository.GetProdsAsync(productParams);
            Response.AddPaginationHeader(products.CurrentPage, products.PageSize, products.TotalCount
                , products.TotalPage);

            var productToReturn = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productToReturn);
        }
        #endregion

        [HttpGet("{id:int}")]
        [Route("GetProductById/{id:int}") ]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productRepository.GetProdByIdAsync(id);

            return _mapper.Map<ProductDto>(product);
        }

        // GET: api/Products/mouse
        [HttpGet("{productname}")]
        [Route("GetProductByName/{id:int}") ]

        public async Task<ActionResult<ProductDto>> GetProductByName(string productname)
        {
            var product = await _productRepository.GetProdByNameAsync(productname);
            return _mapper.Map<ProductDto>(product);

        }

    }
}

