using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class Payment
    {
      

        [Key]
        public int Id { get; set; }
        public string Paymenttype = "PayPal";
        public DateTime? Date { get; set; }
        public double Cost { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

    }
}
