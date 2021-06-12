<<<<<<< HEAD
﻿using System;
=======
﻿using J6BackEnd.Models;
using System;
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
using System.Collections.Generic;

#nullable disable

namespace J6.DAL.Entities
{
    public partial class Brand
    {

        public int BrandId { get; set; }
        public string BrandName { get; set; }
<<<<<<< HEAD
        public string Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
=======

        public string Img { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ProductBrand> ProductBrands { get; set; }
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
    }
}
