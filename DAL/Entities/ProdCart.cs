﻿using System;
using System.Collections.Generic;

#nullable disable

namespace J6.DAL.Entities
{
    public class ProdCart
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int quantity { get; set; } = 1;
        public virtual Cart Cart { get; set; }
        public virtual Product CartNavigation { get; set; }
    }
}
