using J6.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Models
{
    public class OrderWithProducts
    {
        public int OrderId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
