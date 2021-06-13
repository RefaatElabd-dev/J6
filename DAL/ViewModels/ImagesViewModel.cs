using J6.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.DAL.ViewModels
{
    public class ImagesViewModel
    {
        public int ProductId { get; set; }
        public int ImageId { get; set; }

      
        public List<IFormFile> Images { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
