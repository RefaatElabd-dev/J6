using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace J6.DAL.Entities
{
    public class MiddleSavedProduct
    {
        public int ProductId { get; set; }
        public int SaveBagId { get; set; }
        [ForeignKey("ProductId")] public virtual Product Product { get; set; }
        [ForeignKey("SaveBagId")] public virtual SavedBag Bag { get; set; }
    }
}
