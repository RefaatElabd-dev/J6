using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class Order
    {
      
        [Key]
        public int Id { get; set; }
        public int CustimerId { get; set; }
        public OrderStatus Status = OrderStatus.InProgress;
        [ForeignKey("CustimerId")]
        public virtual AppUser Customer { get; set; }
        public double OrderCost { get; set; }
        public virtual ICollection<ProdOrder> ProdOrders { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
