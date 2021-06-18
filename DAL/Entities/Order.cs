using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class Order
    {
        public Order()
        {
            ProdOrders = new HashSet<ProdOrder>();
        }
        [Key]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustimerId { get; set; }
        public int? Rating { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.InProgress;
        public virtual AppUser Customer { get; set; }
        public virtual ICollection<ProdOrder> ProdOrders { get; set; }
    }
}
