using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace J6.DAL.Entities
{
    public class Message
    {
        public int Id { set; get; }
       // [Required]
        public string UserName { set; get; }
        [Required]
        public string Text { set; get; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime When { set; get; }
        [ForeignKey("AppUser")]
        public int  UserID { set; get; }
        [ForeignKey("AppUser")]
        public int sellerId { set; get; }
        public virtual AppUser Sender { set; get; }

        public Message()
        {
            When = DateTime.Now;
        }
    }
}
