using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Helper
{
    public class ProductParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize=(value>MaxPageSize)?MaxPageSize:value;
        }


        public string CurrentUserName { get; set; }



        public string ProductdName { get; set; }
        public string BrandName { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string Color { get; set; }

    }
}
