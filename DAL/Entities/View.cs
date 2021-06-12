using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class View
    {
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        [Key, Column(Order = 0)]

        public int CustomerId { get; set; }
        public string IsFar { get; set; }

        public DateTime CreationDate { get; private set; }

        public virtual Product Product { get; set; }
        public virtual AppUser Customer { get; set; }
    }
}
