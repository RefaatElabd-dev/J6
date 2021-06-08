﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class Cart
    {
        public Cart()
        {
            ProdCarts = new HashSet<ProdCart>();
        }

        public int Cartid { get; set; }
        public string Paymentid { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public int? Cost { get; set; }
        public int? ShippingDetailsId { get; set; }
        [ForeignKey("Customer")]
        public int CustimerId { get; set; }
        public virtual ShippingDetail ShippingDetails { get; set; }
        public virtual ICollection<ProdCart> ProdCarts { get; set; }

        public virtual AppUser Customer { get; set; }
    }
}