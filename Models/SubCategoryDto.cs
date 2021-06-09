using J6.DAL.Entities;
using System;
using System.Collections.Generic;

namespace J6.Models
{
    public class SubCategoryDto
    {
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        // [JsonIgnore]
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductDto> Products { get; set; }
    }
}