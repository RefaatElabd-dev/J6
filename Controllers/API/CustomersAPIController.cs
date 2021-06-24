
using AutoMapper;
using J6.BL.Servises;
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
        private readonly IUserService _userService;

        public CustomersApiController(UserManager<AppUser> userManager, IMapper mapper, DbContainer context, IUserService userService)
        {
            _context = context;
            _userService = userService;
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

        //###################################Handle Address##########################

        [HttpPost]
        [Route("AddUserAddress")]
        //api/CustomersApi/AddUserAddress
        public async Task<Address> AddUserAddress(int UserId, AddressModel model)
        {
            return await _userService.AddUserAddress(UserId, model);
        }

        [HttpPut]
        [Route("EditUserAddress")]
        //api/CustomersApi/EditUserAddress
        public async Task<Address> EditUserAddress(int UserId, AddressModel model)
        {
            return await _userService.EditUserAddress(UserId, model);
        }

        [HttpGet]
        [Route("GetUserAddress/{UserId}")]
        //api/CustomersApi/GetUserAddress/{UserId}
        public async Task<Address> GetUserAddress(int UserId)
        {
            return await _userService.GetUserAddress(UserId);
        }
    }
}
