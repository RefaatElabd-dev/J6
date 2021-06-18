using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class Store
    {
        public Store()
        {
            StoreProducts = new HashSet<StoreProduct>();
        }
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public AppUser Seller { get; set; }

        public virtual ICollection<StoreProduct> StoreProducts { get; set; }
    }
}
