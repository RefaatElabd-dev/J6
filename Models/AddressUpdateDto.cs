using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Models
{
    public class AddressUpdateDto
    {
        public int UserId { set; get; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
    }
}
