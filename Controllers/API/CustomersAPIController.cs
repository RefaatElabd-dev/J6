
using Microsoft.AspNetCore.Http;
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
    public class CustomersApiController : ControllerBase
    {
        //private readonly DataContext _jumia1Context;
        //public CustomersController(DataContext jumia1context)
        //{
        //    _jumia1Context = jumia1context;
        //}
        ////getAllSeller
        ////api/Customers
        //[HttpGet]
        //public List<User> getAllCustomers()
        //{
        //    List<User> users = new List<User>();
        //    //all Customers
        //    var customer = _jumia1Context.Customers.ToList();
        //    foreach (var item in customer)
        //    {
        //        var usercustomer = _jumia1Context.Users.FirstOrDefault(s => s.Userid == item.CustomerId);
        //        users.Add(usercustomer);
        //    }

        //    return users;
        //}



        ////getUserById
        ////api/Customers/1
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetSellerById(int id)
        //{
        //    var customer = await _jumia1Context.Customers.FirstOrDefaultAsync(s =>s.CustomerId==id);
        //    var usercustomer = await _jumia1Context.Users.FirstOrDefaultAsync(s => s.Userid == customer.CustomerId);

        //    return usercustomer;
        //}




        ////editcustomerdata
        ////api/Customers
        //[HttpPut("{id}")]
        //public async Task<ActionResult<User>> Putcustomer(int id, [FromBody] User user)
        //{


        //    if (!CustomerExists(id)) { return NotFound(); }
        //    if (!UserExists(id))
        //    {
        //        return NotFound();
        //    }
        //    var customer = await _jumia1Context.Customers.FirstOrDefaultAsync(s => s.CustomerId == id);
        //    var usercustomer = await _jumia1Context.Users.FirstOrDefaultAsync(s => s.Userid == customer.CustomerId);
        //    usercustomer.FirstName = user.FirstName;
        //    usercustomer.LastName = user.LastName;
        //    usercustomer.Gender = user.Gender;
        //    usercustomer.Email = user.Email;
        //    usercustomer.Password = user.Password;
        //    // userSeller.RoleId = user.RoleId;
        //    usercustomer.UserAddresses = user.UserAddresses;

        //    await _jumia1Context.SaveChangesAsync();
        //    return usercustomer;
        //}




        ////delete  
        ////api/Customers/3
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Deletecustomer(int id)
        //{
        //    var customer = await _jumia1Context.Customers.FindAsync(id);
        //    var customerInUser = await _jumia1Context.Users.FindAsync(customer.CustomerId);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    _jumia1Context.Customers.Remove(customer);
        //    _jumia1Context.Users.Remove(customerInUser);
        //    await _jumia1Context.SaveChangesAsync();

        //    return NoContent();
        //}


        ////check if customer exist

        //private bool CustomerExists(int id)
        //{
        //    return _jumia1Context.Customers.Any(e => e.CustomerId == id);
        //}
        ////check if user exsist
        //private bool UserExists(int id)
        //{
        //    return _jumia1Context.Users.Any(e => e.Userid == id);
        //}

        ///////////////////////////////////////////////////
        ////saved item to specific customer
        ////api/Customers/saveditem/1
        //[HttpGet("{id}")]
        //[Route("saveditem/{id}")]
        //public async Task<ActionResult> GetsaveditemToCustomer(int id)
        //{ //id is customer id
        //    List<object> allsavedproduct = new List<object>();
           
        //    var customer = await _jumia1Context.Customers.FirstOrDefaultAsync(a=>a.CustomerId==id);
        //    if(customer==null)
        //    {
        //        return NotFound();
        //    }
        //    var saveditems = await _jumia1Context.Views.Where(a => a.CustomerId == customer.CustomerId&&a.IsFar=="true").ToListAsync();
        //    foreach(var item in saveditems)
        //    {
        //        var product = await _jumia1Context.Products.Include(a => a.ProductImages).Include(a => a.Promotion).Include(c => c.Reviews).Include(w => w.ShippingDetail).Include(q => q.ProdCarts).Include(p => p.ProdOrders).FirstOrDefaultAsync(a => a.ProductId == item.ProductId);


        //        allsavedproduct.Add(product);
        //    }
        //    return Ok(allsavedproduct);
        //}

        /////////////////////////////////////////////////////
        //recently viewed to specific customer
        //api/Customers/recentlyviewed/1
        //[HttpGet("{id}")]
        //[Route("recentlyviewed/{id}")]
        //public async Task<ActionResult> GetrecentlyviewedToCustomer(int id)
        //{ //id is customer id
        //    List<object> allviewedproduct = new List<object>();
         
        //    var customer = await _jumia1Context.Customers.FirstOrDefaultAsync(a => a.CustomerId == id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }
        //    var saveditems = await _jumia1Context.Views.Where(a => a.CustomerId == customer.CustomerId && a.IsFar == "true").ToListAsync();
        //    foreach (var item in saveditems)
        //    {
        //        var product = await _jumia1Context.Products.Include(a => a.ProductImages).Include(a => a.Promotion).Include(c => c.Reviews).Include(w => w.ShippingDetail).Include(q => q.ProdCarts).Include(p => p.ProdOrders).FirstOrDefaultAsync(a => a.ProductId == item.ProductId);


        //        allsavedproduct.Add(product);
        //    }
        //    return Ok(allsavedproduct);
        //}




    }
}
