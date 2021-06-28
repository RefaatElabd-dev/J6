using J6.BL.Servises;
using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly DbContainer _context;

        public OrdersController(IOrderServices orderServices, DbContainer context)
        {
            _orderServices = orderServices;
            _context = context;
        }
        //Get all orders
        [HttpGet]
        public async Task<IActionResult> getAllOrders()
        {
           //  return await _orderServices.getAllOrders();
            return View(await _orderServices.getAllOrders());
        }
        //Handle Admin Status of orders
        
        //[HttpGet]
        //public async Task<IActionResult> HandleAdminStatus(int OrderId)
        //{
        //    return View(await _orderServices.getAllOrders());
        //}
        [HttpPost]
        public async Task<IActionResult> HandleAdminStatus(int OrderId, [FromBody] int statusNumber)
        {
            _orderServices.HandleAdminStatus(OrderId, statusNumber);
            return View("getAllOrders");
        }
    }
}
