using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace J6.DAL.Entities
{
    public class ShippingDetail
    {
      
        [Key]
        public int Id { get; set; }
        public string PurshesCost { get; set; }
        public int? PaymentId { get; set; }

        public virtual Payment Payment { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
