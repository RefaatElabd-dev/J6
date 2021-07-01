using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace J6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesAPIController : ControllerBase
    {
        private readonly DbContainer _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public SubCategoriesAPIController(DbContainer context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: api/SubCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategories()
        {
            return await _context.SubCategories
                .Select(x => new SubCategory()
                {
                    SubcategoryId = x.SubcategoryId,
                    SubcategoryName = x.SubcategoryName,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Content = x.Content,
                    Category = x.Category,
                    CategoryId = x.CategoryId,
                    Products = x.Products,
                    Image = "images/" + x.Image,
                }).ToListAsync();
            // return await _context.SubCategories.ToListAsync();
        }

        // GET: api/SubCategories/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubCategory>> GetSubCategory(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);

            if (subCategory == null)
            {
                return NotFound();
            }

            return subCategory;
        }

        // PUT: api/SubCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategory(int id, SubCategory subCategory)
        {
            if (id != subCategory.SubcategoryId)
            {
                return BadRequest();
            }

            _context.Entry(subCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SubCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubCategory>> PostSubCategory(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubCategory", new { id = subCategory.SubcategoryId }, subCategory);
        }

        // DELETE: api/SubCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCategoryExists(int id)
        {
            return _context.SubCategories.Any(e => e.SubcategoryId == id);
        }

        //getproducts in subcatgory in category
        //api/SubCategories/categoryproduct/1
        //[HttpGet("{id}")]
        //[Route("categoryproduct/{id}")]

        //public List<Product> getProductOfSubcategory(int id)
        //{//id of category
        //  //  List<Product> products = new List<Product>();
        //    //getsubcategory of id
        //  //  var subcategory = _context.SubCategories.FirstOrDefault(q => q.CategoryId == id);
        //  //  //all product in subcategory
        //  //var  products = _context.Products.Where((a => a.SubcategoryId == subcategory.SubcategoryId)).ToList();

        //    return products;
        //}
        //////////////////////////////////////////////////////////
        /// all product in sub
        //api/SubCategories/categoryproduct/1
        [HttpGet("{id}")]
        [Route("~/CATegoryPRoduct/{id}")]
        public async Task<ActionResult> getProductOfSubcategory(int id)
        {//id of subcategory
            var products = await _context.Products.Where(a => a.SubcategoryId == id).ToListAsync();
            return Ok(products);
        }

        //get all subcategory from category
        //    /categorySubcategory/1
        [HttpGet("{id}")]
        [Route("~/categorySubcategory/{id}")]
        public List<SubCategory> getSubcategoryofCategory(int id)
        {//id is category id

            return _context.SubCategories.Where(q => q.CategoryId == id).ToList(); 

        }


        //get all products from subcategory
        //    /Subcategoryproducts/1
        [HttpGet("{id}")]
        [Route("~/Subcategoryproducts/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> getProductsOFSubcategory(int id)
        {
            List<Product> hproducts = new List<Product>();
            var productss = await _context.Products.Where(q => q.SubcategoryId == id).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(i => i.Reviews).Include(y => y.Views).Take(10).ToListAsync();

            foreach(var item in productss)
            {
                if(item !=null)
                {
                    hproducts.Add(item);
                }
            }
            //for (int i = 0; i < 10; i++)
            //{


            //    highproducts.Add(productss[i]);


            //}
            return hproducts;
        }

        /////////////////////////////////////////////////////////////////////////
        //all color in subcategory
        //  api/SubCategories/allsubcolor/1
        [HttpGet("{id}")]

        [Route("allsubcolor/{id}")]
        public async Task<IActionResult> getColorInSubcategory(int id)
        {
            //id is subcategory id
            List<string> colors = new List<string>();
            var sub = await _context.SubCategories.FindAsync(id);
            if (sub == null)
            {
                return NotFound("wrong id");
            }


            var products = await _context.Products.Where(a => a.SubcategoryId == sub.SubcategoryId).ToListAsync();

            foreach (var item in products)
            {
                if (!colors.Contains(item.Color))
                {
                    if (item.Color != null)
                    {
                        colors.Add(item.Color);
                    }
                }
            }
            return Ok(colors);

        }
        ////////////////////////////////////////////////////////
        ///
          //all price in subcategory
        //  api/SubCategories/allsubprice/1
        [HttpGet("{id}")]

        [Route("allsubprice/{id}")]
        public async Task<IActionResult> getpriceInSubcategory(int id)
        {
            //id is subcategory id
            List<object> prices = new List<object>();
            var sub = await _context.SubCategories.FindAsync(id);
            if (sub == null)
            {
                return NotFound("wrong id");
            }


            var products = await _context.Products.Where(a => a.SubcategoryId == sub.SubcategoryId).ToListAsync();

            foreach (var item in products)
            {
                if (!prices.Contains(item.Price))
                {
                    if (item.Price != 0)
                    {
                        prices.Add(item.Price);
                    }
                }
            }
            return Ok(prices);

        }
        /////////////////////////////////////////////////////
        ///

        //all discount in subcategory
        //  api/SubCategories/allsubdiscount/1
        [HttpGet("{id}")]

        [Route("allsubdiscount/{id}")]
        public async Task<IActionResult> getdiscountInSubcategory(int id)
        {
            //id is subcategory id
            List<object> discount = new List<object>();


            var sub = await _context.SubCategories.FindAsync(id);
            if (sub == null)
            {
                return NotFound("wrong id");
            }


            var products = await _context.Products.Where(a => a.SubcategoryId == sub.SubcategoryId).ToListAsync();

            foreach (var item in products)
            {
                if (item.Discount != null)
                {
                    if (!discount.Contains(item.Discount))
                    {
                        if (item.Discount != null)
                        {
                            discount.Add(item.Discount);
                        }
                    }
                }
            }
            return Ok(discount);

        }
        //////////////////////////////////////
        ///

        //all rating in subcategory
        //  api/SubCategories/allsubrating/1
        [HttpGet("{id}")]

        [Route("allsubrating/{id}")]
        public async Task<IActionResult> getratingInSubcategory(int id)
        {
            //id is subcategory id
            List<object> rating = new List<object>();


            var sub = await _context.SubCategories.FindAsync(id);
            if (sub == null)
            {
                return NotFound("wrong id");
            }


            var products = await _context.Products.Where(a => a.SubcategoryId == sub.SubcategoryId).ToListAsync();

            foreach (var item in products)
            {
                if (item.Rating != null)
                {
                    if (!rating.Contains(item.Rating))
                    {
                        if (item.Rating != null)
                        {
                            rating.Add(item.Rating);
                        }
                    }
                }
            }
            return Ok(rating);

        }




    }
}
