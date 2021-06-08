using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace J6.DAL.Entities
{
    public class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
      //  [JsonIgnore]
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
