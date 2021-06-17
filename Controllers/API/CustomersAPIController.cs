
using AutoMapper;
using J6.DAL.Database;
using J6.DAL.Entities;
using J6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
         private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly DbContainer _context;

        public CustomersApiController(UserManager<AppUser> userManager, IMapper mapper, DbContainer context)
        {
            _context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        //////////////////////////////////////////////////////////////
        //edit customer
        //api/CustomersApi/1
        [HttpPut("{id}")]
        public async Task<ActionResult> editcustomer(int id ,AppUser user)
        {//id is customer id
          var Cusromers = await userManager.GetUsersInRoleAsync("Customer");
          var Cusromer = Cusromers.SingleOrDefault(S => S.Id == id);
          if (Cusromer == null) return NotFound("No customer Matched");
            Cusromer.LastName = user.LastName;
            Cusromer.FirstName = user.FirstName;
            Cusromer.Email = user.Email;
            Cusromer.PhoneNumber = user.PhoneNumber;
            await _context.SaveChangesAsync();
            return Ok(Cusromer);
        }


        /////////////////////////
      //  getcustomerById     //return information about customer 
       // api/CustomersApi/9
        [HttpGet("{id}")]

        public async Task<ActionResult> GetcustomerById(int id)
        {
            var Cusromers = await userManager.GetUsersInRoleAsync("Customer");
            var Cusromer = Cusromers.SingleOrDefault(S => S.Id == id);
            if (Cusromer == null) return NotFound("No customer Matched");
          //  var customerToReturn = mapper.Map<CustomerDto>(Cusromer);
            return Ok(Cusromer);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        /////change user password
        ///api/CustomersApi/
        //[HttpPost("{id}/{oldpassword}")]
        //public async Task<ActionResult> ChangePasswordAsync(int id,[FromBody]string newpassword, string oldpassword)
        //{
        //    var hasoldpassword = BCrypt.Net.BCrypt.HashPassword(oldpassword);

        //    var userr= await _context.Users.FirstOrDefaultAsync(A=>A.Id==id&&A.PasswordHash== hasoldpassword);
        //    if (userr == null) { return NotFound("can't find user"); }
        //    var result = await userManager.RemovePasswordAsync(userr);
        //    if (result.Succeeded)
        //    {
        //        result = await userManager.AddPasswordAsync(userr, newpassword);
        //        await _context.SaveChangesAsync();
        //        return Ok(userr);
        //    }
        //    return NotFound("password not changed");
        //}
        /////change user password
        ///api/CustomersApi/
        [HttpPost("{id}")]
        public async Task<ActionResult> ChangePasswordAsync(int id, [FromBody] string newpassword)
        {

            var userr = await _context.Users.FirstOrDefaultAsync(A => A.Id == id);
            var result = await userManager.RemovePasswordAsync(userr);
            if (result.Succeeded)
            {
                result = await userManager.AddPasswordAsync(userr, newpassword);
                await _context.SaveChangesAsync();
                return Ok(userr);
            }
            return NotFound("password not changed");


        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////


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
