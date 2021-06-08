using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Models
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)] public string Email { get; set; }
        [DataType(DataType.PhoneNumber)] public string PhoneNumber { get; set; }


    }
}
