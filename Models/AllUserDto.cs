using J6.DAL.Entities;
using System.Collections.Generic;

namespace J6.Models
{
    public class AllUserDto
    {
        public string UserName { get; set; }
        public string normalizedUserName { get; set; }
        public string email { get; set; }
        public string normalizedEmail { get; set; }
        public string phoneNumber { get; set; }
        public ICollection<AppRole> Roles { get; set; }
    }
}
