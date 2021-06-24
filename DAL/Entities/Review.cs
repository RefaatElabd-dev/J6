using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class Review
    {
        [Key, Column(Order = 0)]
        public int CustomerId { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public string Comment { get; set; }
        [Range(1.0, 5)]
        public double Rating { get; set; } = 1.0;
        public DateTime CreationTime { get; set; }

        public AppUser Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
