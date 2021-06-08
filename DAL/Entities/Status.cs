using System;
using System.Collections.Generic;

#nullable disable

namespace J6.DAL.Entities
{
    public class Status
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int? OrderId { get; set; }
    }
}
