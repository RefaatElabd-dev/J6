using J6.DAL.Database;
using J6.DAL.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace J6.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CartsItemAPIController : ControllerBase

    {

        private readonly DbContainer _context;

        public CartsItemAPIController(DbContainer context)

        {
            _context = context;
        }



    
    
        ////////////
       //cancal cart
       // /buy cart

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        //To create uniqe cart list for every user
        [HttpPost]
        [Route("~/createcart")]
        public void CreatCartList(Cart shoppingCart)

        {
            _context.Carts.Add(shoppingCart);
            _context.SaveChanges();
        }



        ///////////////////////////////////////////////////////////////////////////
        //add product to cart

        [HttpPost]
        [Route("~/addproducttoCART/{custID}")]
        public async Task<ActionResult> AddToCart([FromBody] Product product, int custID)

        {
            var allproduct = await _context.ProdCarts.ToListAsync();

            // var cartItem = _context.ProdCarts.SingleOrDefault(c => c.CartId == ShoppingCartId && c.ProductId == id);
            var ShoppingCard = await _context.Carts.FirstOrDefaultAsync(a => a.CustimerId == custID);
            if (ShoppingCard == null)
            { return NotFound("not exsit"); }
            ProdCart item = new ProdCart();
            // if (!product.Equals(allproduct)) { return BadRequest("is already "); }

            item.quantity++;
            item.ProductId = product.ProductId;
            item.CartId = ShoppingCard.Cartid;

            var additem = await _context.ProdCarts.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok("added");
        }
        //////////////////////////////////////////////////////////////////////////////////////

        // clear specific  cart remove all peoduct in cart
        [HttpDelete("{cartListID}")]
        [Route("~/deleteCART/{cartListID}")]
        public void ClearCart(int cartListID)
        {
            var cartItems = _context.ProdCarts.Where(cart => cart.CartId == cartListID).ToList();

            _context.ProdCarts.RemoveRange(cartItems);
            _context.SaveChanges();
        }
        /////////////////////////////////////////////////////
        // allproducts in cart
        // api/CartsItem/productsIncart/1


        [HttpGet("{id}")]
        [Route("productsIncart/{id}")]

        public async Task<ActionResult> GetproductsInCart(int id)
        {
            //id is cart id
            //allproduvt in cart

            var allproductincart = await _context.ProdCarts.Where(a => a.CartId == id).ToListAsync();

            if (allproductincart == null)
            {
                return BadRequest();
            }
            List<object> products = new List<object>();


            foreach (var item in allproductincart)
            {
                var oneproduct = await _context.Products.Include(a => a.ProductImages)

                .Include(a => a.Promotion).Include(c => c.Reviews).Include(w => w.ShippingDetail).Include(q => q.ProdCarts).Include(p => p.ProdOrders).FirstOrDefaultAsync(a => a.ProductId == item.ProductId);
                products.Add(oneproduct);

            }

            return Ok(products);

        }


        //////////////////////////////////////////////////////////////////////////

        //api/CartsItem/deleteProductsFromCart/1
        // in body    write    1     only
        //delete one product from cart

        [HttpDelete("{cartid}")]
        [Route("deleteProductsFromCart/{cartid}")]

        public async Task<ActionResult> deleteproductsInCart(int cartid, [FromBody] int productid)
        {
            ProdCart productincart = await _context.ProdCarts.FirstOrDefaultAsync(a => a.CartId == cartid && a.ProductId == productid);

            if (productincart == null)
            {
                return BadRequest();
            }


            _context.ProdCarts.Remove(productincart);

            _context.SaveChanges();
            return Ok(productincart);

        }

        //////////////////////////////////////////////////////////////
        [HttpGet("{cartListID}")]
        [Route("priceofcart/{cartListID}")]
        public double GetShoppingCartTotalPrice(int cartListID)
        {//id is cart id

            double totalPrice = 0;
            var CartItems = _context.ProdCarts.Where(a => a.CartId == cartListID).ToList();

            foreach (var item in CartItems)
            {
                var productsincart = _context.Products.FirstOrDefault(a => a.ProductId == item.ProductId);
                for (int i = 0; i < item.quantity; i++)
                {
                    totalPrice += productsincart.Price;
                }

            }

            return totalPrice;
        }
        //////////////////////////////////////////////////////////
        // to get  cart for specific customer
        [HttpGet("{id}")]
        [Route("~/getcartforCustomer/{id}")]
        public async Task<ActionResult<Cart>> GetShoppingCart(int id)
        {// customer id
            Cart shoppingCart = await _context.Carts.FirstOrDefaultAsync(l => l.CustimerId == id);

            return shoppingCart;
        }
        ////////////////////////////////////////////////////////////////////////
        //edit in cart edit quantity
        [HttpPut("{prodId}/{cartid}")]
        [Route("~/editQuantity/{prodId}/{cartid}")]

        public async Task<ActionResult> editQuantity([FromBody] int quantity, int prodId, int cartid)
        {

            ProdCart productincart = await _context.ProdCarts.FirstOrDefaultAsync(a => a.CartId == cartid && a.ProductId == prodId);
            productincart.quantity = quantity;
            await _context.SaveChangesAsync();

            return Ok(productincart);
        }

        ////////////////////////////////////////////////////////////

        //buys




    }
}
