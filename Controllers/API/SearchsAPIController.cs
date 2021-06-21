
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using J6.DAL.Database;

namespace J6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchsAPiController : ControllerBase
    {
        private readonly DbContainer _context;

        public SearchsAPiController(DbContainer context)
        {
            _context = context;
        }

        //search method
        //api/SearchsAPi/skirt
        [HttpGet("{name}")]
        public async Task<ActionResult> searchbyname(string name)
        {
            List<object> all = new List<object>();
            var products = await _context.Products.Where(e => e.ProductName.Contains(name)).Include(a => a.Promotion).Include(c => c.Reviews).ToListAsync();
           var category = await _context.Categories.Where(e => e.CategoryName.Contains(name)).Include(a => a.SubCategories).ToListAsync();
            var sub = await _context.SubCategories.Where(e => e.SubcategoryName.Contains(name)).Include(a => a.Category).Include(v => v.Products).ToListAsync();
            if (products != null)
            {
                all.Add(products);

                }
                if (category != null)
                {
                    foreach (var i in category)
                    {
                        all.Add(i);
                    }
                }
                if (sub != null)
                {
                    foreach (var i in sub)
                    {
                        all.Add(i);
                    }
                }

                return Ok(all);

            }



        }
}
