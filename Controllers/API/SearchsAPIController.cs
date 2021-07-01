
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using J6.DAL.Database;
using J6.Models;
using AutoMapper;

namespace J6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchsAPiController : ControllerBase
    {
        private readonly DbContainer _context;
        private readonly IMapper _mapper;


        public SearchsAPiController(DbContainer context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //search method
        //api/SearchsAPi/skirt
        [HttpGet("{name}")]
        public async Task<ActionResult> searchbyname(string name)
        {
            List<object> all = new List<object>();
            var products = await _context.Products.Where(e => e.ProductName.Contains(name)).Include(c => c.Reviews).ToListAsync();
           var category = await _context.Categories.Where(e => e.CategoryName.Contains(name)).Include(a => a.SubCategories).ToListAsync();
            var sub = await _context.SubCategories.Where(e => e.SubcategoryName.Contains(name)).Include(a => a.Category).Include(v => v.Products).ToListAsync();
           
             if(category != null)
                {
                 foreach (var i in category)
                    {

                    var subcat = await _context.SubCategories.Where(a => a.CategoryId==i.CategoryId).ToListAsync();
                    foreach(var item in subcat)
                    {
                        if(item!=null)
                        { 
                      var prodd = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).ToListAsync();
                            foreach (var pro in prodd)
                        {

                          if (pro != null) {
                                    var prod = _mapper.Map<ProductDto>(pro);
                                    if (!all.Contains(prod)) { all.Add(prod); }
                                }
                            }
                        }
                    }


                       
                    }
                }


              if (sub != null)
                {
                    foreach (var i in sub)
                    {
                    if (i != null) { 

                    var prodd = await _context.Products.Where(a => a.SubcategoryId == i.SubcategoryId).ToListAsync();
                    foreach (var item in prodd)
                    {
                            if (item != null) {
                                var prod = _mapper.Map<ProductDto>(item);
                                if (!all.Contains(prod)) { all.Add(prod); }
                            }
                        }
                }
                }
            }

         if (products != null)
            {
                foreach (var item in products) { if (!all.Contains(item)) { all.Add(item); } }
            }


            return Ok(all);

            }



        }
}
