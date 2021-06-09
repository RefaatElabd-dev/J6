using AutoMapper;
using J6.DAL.Entities;
using J6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.BL.Mapper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<SubCategory, SubCategoryDto>();




        }
    }
}
