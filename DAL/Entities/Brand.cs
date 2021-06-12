using System;
using System.Collections.Generic;

namespace J6.DAL.Entities
{
    public partial class Brand
    {

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public string Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
