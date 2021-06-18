using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace J6.DAL.Entities
{
    public class Payment
    {
        public Payment()
        {
            ShippingDetails = new HashSet<ShippingDetail>();
        }

        [Key]
        public int Id { get; set; }
        public string Paymenttype { get; set; }
        public DateTime? Date { get; set; }
        public int? Amount { get; set; }

        public virtual ICollection<ShippingDetail> ShippingDetails { get; set; }
    }
}
