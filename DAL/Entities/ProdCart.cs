using System;
using System.Collections.Generic;

#nullable disable

namespace J6.DAL.Entities
{
    public class ProdCart
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
<<<<<<< HEAD
        public int quantity { get; set; }
=======

>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
        public virtual Cart Cart { get; set; }
        public virtual Product CartNavigation { get; set; }
    }
}
