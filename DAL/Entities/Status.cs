using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace J6.DAL.Entities
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string StatusName { get; set; }
        public int? OrderId { get; set; }
    }
}
