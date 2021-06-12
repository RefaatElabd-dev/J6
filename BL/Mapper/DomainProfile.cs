﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using J6.DAL.Entities;
using J6.Models;

namespace J6.BL.Mapper
{
    public class DomainProfile : Profile
    {
     
        
        public DomainProfile()
        {
            CreateMap<AppUser, RegisterDto>();
            CreateMap<AppUser, UserDto>();
            CreateMap<AppUser, AllUserDto>();
            CreateMap<AppUser, SellerDto>();

        }

    }
}
