using J6.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.DAL.ViewModels
{
    public class CategoriesViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IFormFile Image { get; set; }
        public string Content { get; set; }
        //  [JsonIgnore]
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
