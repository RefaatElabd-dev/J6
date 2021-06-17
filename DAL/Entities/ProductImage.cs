using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class ProductImage
    {
      
        [Key]
        public int ImId { get; set; }
        public string ImageUrl { get; set; }

       
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
