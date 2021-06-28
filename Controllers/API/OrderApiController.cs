using J6.BL.Servises;
using J6.DAL.Database;
using J6.DAL.Entities;
using J6.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly DbContainer _context;

        public OrderApiController(IOrderServices orderServices, DbContainer context)
        {
            _orderServices = orderServices;
            _context = context;
        }

        [HttpPost]
        [Route("approveOrder/{CustomerId}")]
        public async Task approveOrder(int CustomerId)
        {
            await _orderServices.approveOrder(CustomerId);
        }

        [HttpGet]
        [Route("getAllOrders")]
        public async Task<ICollection<Order>> getAllOrders()
        {
            return await _orderServices.getAllOrders();
        }

        [HttpPost]
        [Route("HandleAdminStatus/{OrderId}")]
        public void HandleAdminStatus(int OrderId, [FromBody]int statusNumber)
        {
            _orderServices.HandleAdminStatus(OrderId, statusNumber);
        }

        [HttpGet]
        [Route("SwitchCartToOrder/{CustomerId}")]
        public async Task SwitchCartToOrder(int CustomerId)
        {
            await _orderServices.SwitchCartToOrder(CustomerId);
        }


        [HttpGet]
        [Route("getAllProductsWithOrderId/{orderId}")]
        public async Task<ActionResult<IEnumerable<Product>>> getAllProductsWithOrderIdAsync(int orderId)
        {
            IEnumerable<Product> prodcts =  await _orderServices.getAllProductsWithOrderIdAsync(orderId);
            return prodcts == null ? NotFound("There are no Products For This User") :
                                    Ok(prodcts);
        }

        [HttpGet]
        [Route("getAllProductsWithCustomerId/{customerId}")]
        public async Task<ActionResult<IEnumerable<Product>>> getAllProductsWithCustomerIdAsync(int customerId)
        {
            IEnumerable<Product> prodcts = await _orderServices.getAllProductsWithCustomerIdAsync(customerId);
            return prodcts == null ? NotFound("There are no Products For This User") :
                                    Ok(prodcts);
        }

        [HttpGet]
        [Route("getOrderProductsInStatus/{customerId}/{statusNumber}")]
        public async Task<ActionResult<IEnumerable<OrderWithProducts>>> getOrderProductsInStatusAsync(int customerId, int statusNumber )
        {
            IEnumerable<OrderWithProducts> prodcts = await _orderServices.getOrderProductsInStatusAsync(customerId, statusNumber);
            return prodcts == null ? NotFound("There are no Orders For This User") :
                                    Ok(prodcts);
        }
    }
}
