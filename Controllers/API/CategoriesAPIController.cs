using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using J6.DAL.Database;
using J6.DAL.Entities;

namespace J6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesAPiController : ControllerBase
    {
        private readonly DbContainer _context;

        public CategoriesAPiController(DbContainer context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.Include(a => a.SubCategories).ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }

        //getsubcatogryand product of subcategory
        // api/Categories/categoryproduct/1
        [HttpGet]

        [Route("categoryproduct/{id}")]
        public async Task<IActionResult> getsubcatogryandproductofsubcategory( int id)
        {
            
            var category = await _context.Categories.FindAsync(id);
            if (category==null)
            {
                return NotFound("wrong id");
            }
           

            var subandproduct=await _context.SubCategories.Where(a => a.CategoryId == category.CategoryId).Include(z=>z.Products).ToListAsync();
            

            return Ok(subandproduct);

        }
        //////////////////////////////////////////////////
        //filters
        // get products of category using color

        // api/Categories/color/1
        //in body write    "red"
        [HttpGet("{id}")]
        [Route("color/{id}")]
        public async Task<ActionResult> GetproductCategoryusingcolor([FromBody] string color, int id)
        {
            //id is category id
            List<object> allproducts = new List<object>();
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var allSubInCategory = _context.SubCategories.Where(a => a.CategoryId == category.CategoryId);
            foreach (var item in allSubInCategory)
            {

                var products = _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId && a.Color == color).Include(a => a.Promotion).Include(a => a.ShippingDetail).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.StoreProducts);

                allproducts.Add(products);

            }

            return Ok(allproducts);
        }


        //////////////////////////////////////////
        // get products of category using color

        // api/Categories/price/1
        //in body write    "40"
        [HttpGet("{id}")]
        [Route("price/{id}")]
        public async Task<ActionResult> GetproductCategoryusingprice([FromBody] double price, int id)
        {//id is category id
            List<object> allproducts = new List<object>();
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var allSubInCategory = await _context.SubCategories.Where(a => a.CategoryId == category.CategoryId).ToListAsync();
            foreach (var item in allSubInCategory)
            {
                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId && a.Price == price).Include(a => a.Promotion).Include(a => a.ShippingDetail).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.StoreProducts).ToListAsync();

                if (products != null)
                {

                    allproducts.Add(products);
                }

            }

            return Ok(allproducts);
        }


        ////////////////
        // // get products of category using discount
        // api/Categories/discount/1
        //in body write    40
        //20 40 60 80
        [HttpGet("{id}")]
        [Route("discount/{id}")]
        public async Task<ActionResult> GetproductCategoryusingdiscount([FromBody] int discount, int id)
        {//id is category id
            List<object> allproducts = new List<object>();

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var allSubInCategory = await _context.SubCategories.Where(a => a.CategoryId == category.CategoryId).ToListAsync();
            foreach (var item in allSubInCategory)
            {

                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).Include(a => a.Promotion).Include(a => a.ShippingDetail).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.StoreProducts).ToListAsync();

                if (products != null)
                {
                    foreach (var pro in products)
                    {

                        if (pro.Discount >= discount && pro.Discount < discount + 20)
                        {
                            allproducts.Add(pro);
                        }

                    }


                }

            }



            return Ok(allproducts);
        }



        //////////////////////////////////////////////////
        ///  ////////////////
        // // get products of category using discount
        // api/Categories/rating/1
        //in body write    4
        [HttpGet("{id}")]
        [Route("rating/{id}")]
        public async Task<ActionResult> GetproductCategoryusingrating([FromBody] double rating, int id)
        {//id is category id
            List<object> allproducts = new List<object>();
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var allSubInCategory = await _context.SubCategories.Where(a => a.CategoryId == category.CategoryId).ToListAsync();
            foreach (var item in allSubInCategory)
            {

                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).Include(a => a.Promotion).Include(a => a.ShippingDetail).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.StoreProducts).ToListAsync();

                if (products != null)
                {
                    foreach (var pro in products)
                    {

                        if (pro.Rating < rating + 1 && pro.Rating >= rating)
                        {
                            allproducts.Add(pro);
                        }


                    }


                }

            }


            return Ok(allproducts);

        }


        ///////////////////////////////////////////////////////////
        //filter using brand
        // api/Categories/brand/1
        //in body write    1
        [HttpGet("{id}")]
        [Route("brand/{id}")]
        public async Task<ActionResult> GetproductCategoryusingbrand([FromBody] int brand, int id)//String BrandName  insted of Brand
        {//id is category id
            List<object> allproducts = new List<object>();
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var allSubInCategory = await _context.SubCategories.Where(a => a.CategoryId == category.CategoryId).ToListAsync();
            foreach (var item in allSubInCategory)
            {
                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId /*&& a.BrandId == brand*/).Include(a => a.Promotion).Include(a => a.ShippingDetail).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.StoreProducts).ToListAsync();

                if (products != null)
                {

                    allproducts.Add(products);
                }

            }

            return Ok(allproducts);
        }


        //filter using size
        // api/Categories/size/1
        //in body write    "xl"
        [HttpGet("{id}")]
        [Route("size/{id}")]
        public async Task<ActionResult> GetproductCategoryusingsize([FromBody] string size, int id)
        {//id is category id
            List<object> allproducts = new List<object>();
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var allSubInCategory = await _context.SubCategories.Where(a => a.CategoryId == category.CategoryId).ToListAsync();
            foreach (var item in allSubInCategory)
            {
                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId && a.Size==size).Include(a => a.Promotion).Include(a => a.ShippingDetail).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.StoreProducts).ToListAsync();

                if (products != null)
                {

                    allproducts.Add(products);
                }

            }

            return Ok(allproducts);
        }


        /////////////////////////////////////////////////////////////////////////////


        //get all product in brand

        //in home
        //   api/Categories/allProductBrand/1

        [HttpGet("{id}")]
        [Route("allProductBrand/{id}")]
        public async Task<ActionResult> GetallproductInBrand(string brandName)
        {//id is brand id
           
            var products=await _context.Products.Where(a=>a.BrandName==brandName).Include(a => a.Promotion).Include(a => a.ShippingDetail).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q=>q.Subcategory).ToListAsync();
          
            return Ok(products);

        }

        /////////////////////////////////////////

        //get all subcategory in brand

        //in home
        //   api/Categories/allsubcategoryBrand/1

        //[HttpGet("{id}")]
        //[Route("allsubcategoryBrand/{id}")]
        //public async Task<ActionResult> GetallsubcategoryInBrand(int id)
        //{//id is brand id
        //    List<SubCategory> sub = new List<SubCategory>();
        //    var products = await _context.Products.Where(a => a.BrandId == id).Include(a => a.Promotion).Include(a => a.ShippingDetail).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).ToListAsync();
        //    foreach (var item in products)
        //    {
        //        var subcategory = await _context.SubCategories.FirstOrDefaultAsync(a => a.SubcategoryId == item.SubcategoryId);
        //        if(!sub.Contains(subcategory))
        //        {
        //            if (subcategory != null)
        //            {
        //                sub.Add(subcategory);
        //            }
        //        }
               
        //    }

        //    return Ok(sub);

        //}

        //////////////////////////////////////////////////////////////

        //get all brand in subcategory

        //   api/Categories/allBrandInsubcategory/1

        //[HttpGet("{id}")]
        //[Route("allBrandInsubcategory/{id}")]
        //public async Task<ActionResult> GetallBrandInsubcategory(int id)
        //{//id is subcategory id
        //    List<object> brand = new List<object>();
        //    var products = await _context.Products.Where(a => a.SubcategoryId==id).Include(a => a.Promotion).Include(a => a.ShippingDetail).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).ToListAsync();
        //    foreach (var item in products)
        //    {
        //        var onebrand = await _context.Brands.FirstOrDefaultAsync(a => a.BrandId == item.BrandId);
        //        if (!brand.Contains(onebrand))
        //        {
        //            if (onebrand != null)
        //            {
        //                brand.Add(onebrand);
        //            }
        //        }

        //    }

        //    return Ok(brand);

        //}



        //////////////////////////////
        //get all brand in category

        //in home
        //   api/Categories/allBrandIncategory/1

        //[HttpGet("{id}")]
        //[Route("allBrandIncategory/{id}")]
        //public async Task<ActionResult> GetallBrandIncategory(int id)
        //{//id is category id
        //    List<object> brand = new List<object>();

        //    var sub = await _context.SubCategories.Where(a => a.CategoryId == id).ToListAsync();
        //    foreach (var item in sub)
        //    {

        //        var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).ToArrayAsync();
               
        //            foreach (var bran in products) { 
        //            var onebrand = await _context.Brands.FirstOrDefaultAsync(a => a.BrandId == bran.BrandId);
        //            if (!brand.Contains(onebrand)) {
        //                if(onebrand != null) { 
        //                brand.Add(onebrand);
        //                }
        //            }
                          
                    
        //        }


        //    }

        //    return Ok(brand);

        //}

     




    }
}
