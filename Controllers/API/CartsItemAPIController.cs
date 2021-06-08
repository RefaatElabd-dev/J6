using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsItemAPiController : ControllerBase
    {

        private readonly DbContainer _context;

        public CartsItemAPiController(DbContainer context)
        {
            _context = context;
        }


        //  allproducts in cart
       // api/CartsItem/productsIncart/1
       
        [HttpGet("{id}")]
        [Route("productsIncart/{id}")]

        public async Task<ActionResult> GetproductsInCart(int id)
        {
            //id is cart id
            //allproduvt in cart
            var allproductincart =await _context.ProdCarts.Where(a => a.CartId == id).ToListAsync();
            if (allproductincart == null)
            {
                return BadRequest();
            }
            List<object> products = new List<object>();
          
            foreach (var item in allproductincart)
            {
                var oneproduct =await _context.Products.Include(a => a.ProductImages)
                .Include(a => a.Promotion).Include(c => c.Reviews).Include(w => w.ShippingDetail).Include(q => q.ProdCarts).Include(p => p.ProdOrders).FirstOrDefaultAsync(a => a.ProductId == item.ProductId);
                products.Add(oneproduct);

            }

            return Ok(products);

        }
        //api/CartsItem/deleteProductsFromCart/1
      // in body    write    1     only
        //delete product from cart

        [HttpDelete("{cartid}")]
        [Route("deleteProductsFromCart/{cartid}")]
        public async Task<ActionResult> deleteproductsInCart( int cartid, [FromBody] int productid)
        {
            ProdCart productincart =await _context.ProdCarts.FirstOrDefaultAsync(a => a.CartId == cartid && a.ProductId == productid);
            if (productincart == null)
            {
                return BadRequest();
            }

          

            _context.ProdCarts.Remove(productincart);

            _context.SaveChanges();
            return Ok(productincart);

        }


   















    }
}
