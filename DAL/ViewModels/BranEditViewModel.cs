using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace J6.DAL.ViewModels
{
    public class BranEditViewModel
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public string Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }    
    }
}
