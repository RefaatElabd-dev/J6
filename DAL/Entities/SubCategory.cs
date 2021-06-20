using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace J6.DAL.Entities
{
    public class SubCategory
    {
        public SubCategory()
        {
            Products = new HashSet<Product>();
        }

        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
