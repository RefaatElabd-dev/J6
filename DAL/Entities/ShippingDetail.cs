using System;
using System.Collections.Generic;

#nullable disable

namespace J6.DAL.Entities
{
    public class ShippingDetail
    {
        public ShippingDetail()
        {
            Carts = new HashSet<Cart>();
        }

        public int ShippingDetailsId { get; set; }
        public string PurshesCost { get; set; }
        public int? PaymentId { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual Product ShippingDetails { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
