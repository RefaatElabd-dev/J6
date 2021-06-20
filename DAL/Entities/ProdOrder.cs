using System;
using System.Collections.Generic;

#nullable disable

namespace J6.DAL.Entities
{
    public class ProdOrder
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int quantity { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
