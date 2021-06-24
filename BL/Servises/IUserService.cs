using J6.DAL.Entities;
using J6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.BL.Servises
{
    public interface IUserService
    {
        public Task<Address> AddUserAddress(int UserId, AddressModel model);
        public Task<Address> EditUserAddress(int UserId, AddressModel model);
        public Task<Address> GetUserAddress(int UserId);
    }
}
