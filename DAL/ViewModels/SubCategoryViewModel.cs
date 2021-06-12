using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.DAL.ViewModels
{
    public class SubCategoryViewModel
    {

        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
    }
}
