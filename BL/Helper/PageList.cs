using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Helper
{
    public class PageList<T>:List<T>
    {
        public PageList(IEnumerable<T> items,int count,int PageNumber, int pageSize)
        {
            CurrentPage = PageNumber;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);     
            PageSize = pageSize;
            TotalCount = Count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize) 
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).ToListAsync();
            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }
}
