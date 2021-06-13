using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class ProductImage
    {
        public int ProductId { get; set; }
        public int ImageId { get; set; }
        public string Image { get; set; }

        public virtual Product Product { get; set; }
    }
}
