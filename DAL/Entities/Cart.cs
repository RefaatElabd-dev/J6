﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public double Cost { get; set; }
        public int CustimerId { get; set; }
        public virtual ICollection<ProdCart> ProdCarts { get; set; }
        [ForeignKey("CustimerId")]
        public virtual AppUser Customer { get; set; }
    }
}
