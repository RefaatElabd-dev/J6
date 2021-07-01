using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Models
{
    public class SellerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual int Id {get;set;}
        [DataType(DataType.EmailAddress)] public string Email { get; set; }
    }
}
