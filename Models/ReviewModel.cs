using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Models
{
    public class ReviewModel
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
        [Range(1.0,5)]
        public double Rating { get; set; } = 1.0;
    }
}
