using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class Promotion
    {
        public Promotion()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int PromotionId { get; set; }
        [Range(0.0,0.99)]
        public double? Discount { get; set; }
        public string Description { get; set; }
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public virtual AppUser Seller { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
