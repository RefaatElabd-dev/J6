using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class View
    {
        public int CustomerId { get; set; }      
        public virtual AppUser Customer { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public DateTime CreationDate { get; private set; }
    }
}
