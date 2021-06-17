using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace J6.DAL.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            Messages = new HashSet<Message>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public ICollection<AppUserRole> userRoles { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<View> Views { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Order Order { get; set; }
        public virtual Store Store { get; set; }
        public virtual SavedBag Bag { get; set; }
        ///////
        public virtual ICollection<Message> Messages { get; set; }




    }
}
