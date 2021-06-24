using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace J6.DAL.Entities
{
    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        [Key]
        public int UserId { get; set; }
        [ForeignKey("UserId")] public AppUser AppUser { get; set; }

        public Address() { }
        public Address(string country, string city, string street)
        {
            City = city;
            Country = country;
            Street = street;
        }
    }
}
