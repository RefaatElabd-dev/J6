using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.DAL.Entities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser user { get; set; }
        public AppRole Role { get; set; }
    }
}
