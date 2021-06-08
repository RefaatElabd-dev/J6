using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace J6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesAPIController : ControllerBase
    {
        private readonly DbContainer _context;

        public SubCategoriesAPIController(DbContainer context)
        {
            _context = context;
        }

        // GET: api/SubCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategories()
        {
            return await _context.SubCategories.ToListAsync();
        }

        // GET: api/SubCategories/5
        [HttpGet("{id}")]
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
        [HttpGet("{id}")]
     [Route("categoryproduct/{id}")]
 
        public List<Product> getProductOfSubcategory(int id)
            {//id of category
            List<Product> products=new List<Product>();
            //getsubcategory of id
            var subcategory =  _context.SubCategories.FirstOrDefault(q=>q.CategoryId==id);
            //all product in subcategory
            products = _context.Products.Where((a => a.SubcategoryId == subcategory.SubcategoryId)).ToList();
            
            return products;
            }
        //get all subcategory from category
        //    /categorySubcategory/1
        [HttpGet("{id}")]
        [Route("~/categorySubcategory/{id}")]
        public  List<SubCategory> getSubcategoryofCategory(int id)
        {//id is category id
          
            return  _context.SubCategories.Where(q => q.CategoryId == id).ToList(); ;

        }


        //get all products from subcategory
        //    /Subcategoryproducts/1
        [HttpGet("{id}")]
        [Route("~/Subcategoryproducts/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> getProductsOFSubcategory(int id)
        {
            List<Product> highproducts = new List<Product>();
            var productss=await _context.Products.Where(q => q.SubcategoryId == id).Include(a => a.Promotion).Include(a=>a.ShippingDetail).Include(c=>c.ProdCarts).Include(p=>p.ProdOrders).Include(o=>o.ProductImages).Include(i=>i.Reviews).Include(e=>e.StoreProducts).Include(y=>y.Views).ToListAsync();

            for (int i = 0; i < 10; i++)
            {


                highproducts.Add(productss[i]);


            }
            return highproducts;
        }






    }
}
