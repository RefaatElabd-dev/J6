
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
        public async Task<ActionResult> editcustomer(int id, AppUser user)
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
            object all = new object();
            var Cusromers = await userManager.GetUsersInRoleAsync("Customer");
            var Cusromer = Cusromers.SingleOrDefault(S => S.Id == id);
            if (Cusromer == null) return NotFound("No customer Matched");
            return Ok(Cusromer);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        /////change user password
        ///api/CustomersApi/
        [HttpPost("{oldpassword}/{newpassword}")]
        public async Task<ActionResult> ChangePassword(AppUser user, string oldpassword, string newpassword)
        {
            var userr = await _context.Users.FirstOrDefaultAsync(A => A.Id == user.Id);
            if (userr == null) { return NotFound("can't find user"); }
            return Ok(await userManager.ChangePasswordAsync(userr, oldpassword, newpassword));
        }
        /////////////////////////////////////////////
        /////edit customer address
        //api/CustomersApi/editaddress/10

        [HttpPost]
        [Route("editaddresscustomer")]
        public async Task<ActionResult> editaddresscustomer(AddressUpdateDto addressUpdateDto)
        {
            //id is customer id
            var User = await userManager.FindByIdAsync(addressUpdateDto.UserId.ToString());
            if (User == null) return NotFound("No customer Matched");
            User.Address = new Address(addressUpdateDto.Country, addressUpdateDto.City, addressUpdateDto.Street);
            await userManager.UpdateAsync(User);
            return Ok(User);
        }
    }
}
