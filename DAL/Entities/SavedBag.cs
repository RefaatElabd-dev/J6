using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace J6.DAL.Entities
{
    public class SavedBag
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")] public virtual  AppUser Customer { get; set; }
        public virtual ICollection<MiddleSavedProduct> ProductsBag { get; set; }
    }
}
