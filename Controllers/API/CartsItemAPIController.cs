﻿using J6.DAL.Database;
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

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        //To create uniqe cart list for every user
        [HttpPost]
        [Route("createcart")]
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
            var ShoppingCard = await _context.Carts.FirstOrDefaultAsync(a => a.CustimerId == custID);
            if (ShoppingCard == null)
            { return NotFound("not exsit"); }
            ProdCart item = new ProdCart();
           // item.quantity++;
            item.ProductId = product.Id;
            item.CartId = ShoppingCard.Id;
            var additem = await _context.ProdCarts.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok("added");
        }
        //////////////////////////////////////////////////////////////////////////////////////

        // clear specific  cart remove all peoduct in cart
        [HttpDelete("{cartListID}")]
        [Route("deleteCART/{cartListID}")]
        public void ClearCart(int cartListID)
        {
            var cartItems = _context.ProdCarts.Where(cart => cart.CartId == cartListID).ToList();

            _context.ProdCarts.RemoveRange(cartItems);
            _context.SaveChanges();
        }
        /////////////////////////////////////////////////////
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


            return Ok(allproductincart);

        }
        //////////////////////////////////////////////////////////////////////////

        //api/CartsItem/deleteProductsFromCart/1
        // in body    write    1     only
        //delete one product from cart

        [HttpDelete("{cartid}")]
        [Route("deleteProductsFromCart/{cartid}")]

        public async Task<ActionResult> deleteproductsInCart(int cartid, int productid)
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
        //api/CartsItemAPI/priceOfcart/{cartId}

        [HttpGet("{cartListID}")]
        [Route("priceofcart/{cartListID}")]
        public async Task<ActionResult> GetShoppingCartTotalPrice(int cartListID)
        {
            double totalPrice = 0;
            var CartItems = await _context.ProdCarts.Where(a => a.CartId == cartListID).ToArrayAsync();

            foreach (var item in CartItems)
            {
                var productsincart = await _context.Products.FirstOrDefaultAsync(a => a.Id == item.ProductId);
                var element = await _context.ProdCarts.FirstOrDefaultAsync(a => a.ProductId == productsincart.Id && a.CartId == cartListID);

                for (int i = 0; i < element.quantity; i++)
                {
                    if (productsincart.Discount != 0)
                    {
                        totalPrice += (1 - productsincart.Discount) * productsincart.Price;
                    }
                    else
                    {
                        totalPrice += productsincart.Price;
                    }
                }
            }
            totalPrice = ((int)(totalPrice * 100)) / 100.0;

            Cart cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartListID);
            cart.Cost = totalPrice;
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return Ok(totalPrice);
        }

            ////////////////////////////////////////////////////////////
            // to get  cart for specific customer
            [HttpGet("{id}")]
        [Route("getcartforCustomer/{id}")]
        public async Task<ActionResult<Cart>> GetShoppingCart(int id)
        {// customer id
            Cart shoppingCart = await _context.Carts.FirstOrDefaultAsync(l => l.CustimerId == id);

            return shoppingCart;
        }

        //api/CartsItemAPi/DeleteCartProductsForCustomer/{id}
        [HttpDelete("{id}")]
        [Route("DeleteCartProductsForCustomer/{id}")]
        public async Task<ActionResult> DeleteCartProductsForCustomer(int id)
        {// customer id
            int CartId = await _context.Carts.Where(l => l.CustimerId == id).Select(C=>C.Id).FirstOrDefaultAsync();
            List<ProdCart> productsInCart = await _context.ProdCarts.Where(P => P.CartId == CartId).ToListAsync();
            _context.ProdCarts.RemoveRange(productsInCart);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        ////////////////////////////////////////////////////////////////////////
        //edit in cart edit quantity
        [HttpPut("{prodId}/{cartid}")]
        [Route("editQuantity/{prodId}/{cartid}")]

        public async Task<ActionResult> editQuantity(int quantity, int prodId, int cartid)
        {

            ProdCart productincart = await _context.ProdCarts.FirstOrDefaultAsync(a => a.CartId == cartid && a.ProductId == prodId);
            productincart.quantity = quantity;
            await _context.SaveChangesAsync();

            return Ok(productincart);
        }

        ////////////////////////////////////////////////////////////
        // increase quantity
        [HttpPut("{prodId}/{cartid}")]
        [Route("~/increaseQuantity/{prodId}/{cartid}")]

        public async Task<ActionResult> increaseQuantity(int prodId, int cartid)
        {

            ProdCart productincart = await _context.ProdCarts.FirstOrDefaultAsync(a => a.CartId == cartid && a.ProductId == prodId);
            productincart.quantity++;
            await _context.SaveChangesAsync();

            return Ok(productincart);
        }
        //////////////////////////////////////////////////////////////////////////
        ///// decrease quantity
        [HttpPut("{prodId}/{cartid}")]
        [Route("~/decreaseQuantity/{prodId}/{cartid}")]

        public async Task<ActionResult> decreaseQuantity(int prodId, int cartid)
        {

            ProdCart productincart = await _context.ProdCarts.FirstOrDefaultAsync(a => a.CartId == cartid && a.ProductId == prodId);
            productincart.quantity--;
            await _context.SaveChangesAsync();

            return Ok(productincart);
        }



    }
}
