using J6.DAL.Database;
using J6.DAL.Entities;
using J6.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.BL.Servises
{
    public class UserService : IUserService
    {
        private readonly DbContainer _context;
        private readonly UserManager<AppUser> _userManager;

        public UserService(DbContainer context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<Address> AddUserAddress(int UserId, AddressModel model)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user == null) throw new Exception("Check if This User Exists");
            Address address = await _context.Addresses.Where(A => A.UserId == UserId).FirstOrDefaultAsync();
            if(address == null)
            {
                address = new Address(model.Country, model.City, model.Street);
                address.UserId = UserId;
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
                return address;
            }
            throw new Exception("This User already has an Address Try To Edit It");
        }

        public async Task<Address> EditUserAddress(int UserId, AddressModel model)
        {
            Address address = await _context.Addresses.Where(A => A.UserId == UserId).FirstOrDefaultAsync();
            if (address != null)
            {
                address.Country = model.Country;
                address.City = model.City;
                address.Street = model.Street;
                _context.Addresses.Update(address);
                await _context.SaveChangesAsync();
                return address;
            }
            else
            {
                return await AddUserAddress(UserId, model);
            }
        }

        public async Task<Address> GetUserAddress(int UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user == null) throw new Exception("Check if This User Exists");
            Address address = await _context.Addresses.Where(A => A.UserId == UserId).FirstOrDefaultAsync();
            if(address == null) throw new Exception("This User Has no Address Yet, Try To Add one.");
            return address;
        }
    }
}
