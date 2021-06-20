using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Hosting;

namespace J6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesAPiController : ControllerBase
    {
        private readonly DbContainer _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CategoriesAPiController(DbContainer context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories
                .Select(x => new Category()
                {
                  CategoryId   = x.CategoryId,
                    CategoryName = x.CategoryName,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Content = x.Content,
                    SubCategories=x.SubCategories,
                    Image = "images/" + x.Image,
                }).ToListAsync();
            // return await _context.Categories.Include(a => a.SubCategories).ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [Route("category/{id}")]
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
        public async Task<IActionResult> getsubcatogryandproductofsubcategory(int id)
        {

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound("wrong id");
            }


            var subandproduct = await _context.SubCategories.Where(a => a.CategoryId == category.CategoryId).Include(z => z.Products).ToListAsync();


            return Ok(subandproduct);

        }
        //////////////////////////////////////////////////
        //filters
        // get products of category using color

        // api/Categories/color/1?color=red
        //in body write    "red"
        [HttpGet("{id}")]
        [Route("color/{id}")]

        public async Task<ActionResult> GetproductCategoryusingcolor(string color, int id)

        {
            //id is category id
            List<object> allproducts = new List<object>();
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var allSubInCategory = await _context.SubCategories.Where(a => a.CategoryId == category.CategoryId).ToListAsync();
            foreach (var item in allSubInCategory)
            {

                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId && a.Color == color).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.StoreProducts).ToArrayAsync();

                if (products != null)
                {
                    foreach (var oitem in products)
                    {
                        if (oitem != null)
                        {
                            allproducts.Add(oitem);
                        }
                    }
                }

            }

            return Ok(allproducts);
        }


        //////////////////////////////////////////

        // get products of category using price


        // api/Categories/price/1
        //in body write    "40"
        [HttpGet("{id}")]
        [Route("price/{id}")]

        public async Task<ActionResult> GetproductCategoryusingprice(double price, int id)

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
                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId && a.Price == price).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.StoreProducts).ToListAsync();

                if (products != null)
                {
                    foreach (var oitem in products)
                    {
                        if (oitem != null)
                        {
                            allproducts.Add(oitem);
                        }
                    }


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

        public async Task<ActionResult> GetproductCategoryusingdiscount(int discount, int id)

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

                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.StoreProducts).ToListAsync();

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

        public async Task<ActionResult> GetproductCategoryusingrating(double rating, int id)

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

                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.StoreProducts).ToListAsync();

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

        public async Task<ActionResult> GetproductCategoryusingbrand(int brand, int id)//String BrandName  insted of Brand

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
                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId && a.BrandId == brand).Include(o => o.ProductImages).ToListAsync();
                if (products != null)
                {
                    foreach (var oitem in products)
                    {
                        if (oitem != null)
                        {
                            allproducts.Add(oitem);
                        }
                    }
                }
            }
            return Ok(allproducts);
        }


        //filter using size
        // api/Categories/size/1
        //in body write    "xl"
        [HttpGet("{id}")]
        [Route("size/{id}")]

        public async Task<ActionResult> GetproductCategoryusingsize(Size size, int id)

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
                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId && a.Size == size).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.StoreProducts).ToListAsync();

                if (products != null)
                {
                    foreach (var oitem in products)
                    {
                        if (oitem != null) { allproducts.Add(oitem); }
                    }
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
        public async Task<ActionResult> GetallproductInBrand(int id)
        {//id is brand id

            var products = await _context.Products.Where(a => a.BrandId == id).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).Include(q => q.Subcategory).ToListAsync();

            return Ok(products);

        }


        /////////////////////////////////////////

        //get all subcategory in brand

        //in home
        //   api/Categories/allsubcategoryBrand/1


        [HttpGet("{id}")]
        [Route("allsubcategoryBrand/{id}")]
        public async Task<ActionResult> GetallsubcategoryInBrand(int id)
        {//id is brand id
            List<SubCategory> sub = new List<SubCategory>();
            var products = await _context.Products.Where(a => a.BrandId == id).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).ToListAsync();
            foreach (var item in products)
            {
                var subcategory = await _context.SubCategories.FirstOrDefaultAsync(a => a.SubcategoryId == item.SubcategoryId);
                if (!sub.Contains(subcategory))
                {
                    if (subcategory != null)
                    {
                        sub.Add(subcategory);
                    }
                }

            }

            return Ok(sub);

        }


        //////////////////////////////////////////////////////////////

        //get all brand in subcategory

        //   api/Categories/allBrandInsubcategory/1


        [HttpGet("{id}")]
        [Route("allBrandInsubcategory/{id}")]
        public async Task<ActionResult> GetallBrandInsubcategory(int id)
        {//id is subcategory id
            List<object> brand = new List<object>();
            var products = await _context.Products.Where(a => a.SubcategoryId == id).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(e => e.StoreProducts).Include(y => y.Views).ToListAsync();
            foreach (var item in products)
            {
                var onebrand = await _context.Brands.FirstOrDefaultAsync(a => a.BrandId == item.BrandId);
                if (!brand.Contains(onebrand))
                {
                    if (onebrand != null)
                    {
                        brand.Add(onebrand);
                    }
                }

            }

            return Ok(brand);

        }




        //////////////////////////////
        //get all brand in category

        //in home
        //   api/Categories/allBrandIncategory/1


        [HttpGet("{id}")]
        [Route("allBrandIncategory/{id}")]
        public async Task<ActionResult> GetallBrandIncategory(int id)
        {//id is category id
            List<object> brand = new List<object>();

            var sub = await _context.SubCategories.Where(a => a.CategoryId == id).ToListAsync();
            foreach (var item in sub)
            {

                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).ToArrayAsync();

                foreach (var bran in products)
                {
                    var onebrand = await _context.Brands.FirstOrDefaultAsync(a => a.BrandId == bran.BrandId);

                    if (!brand.Contains(onebrand))
                    {
                        if (onebrand != null)
                        {
                            brand.Add(onebrand);
                        }
                    }


                }


            }

            return Ok(brand);

        }
        ////////////////////////////////////////////////////////
        ///
        //all color in category
        //  api/Categories/allcategorycolor/1
        [HttpGet("{id}")]

        [Route("allcategorycolor/{id}")]
        public async Task<IActionResult> getallcolorincategory(int id)
        {
            //id is category id
            List<string> colors = new List<string>();
            var sub = await _context.SubCategories.Where(a => a.CategoryId == id).ToListAsync();
            if (sub == null)
            {
                return NotFound("wrong id");
            }


            foreach (var item in sub)
            {
                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).ToListAsync();
                foreach (var pro in products)
                {
                    if (!colors.Contains(pro.Color))
                    {
                        if (pro.Color != null)
                        {
                            colors.Add(pro.Color);
                        }
                    }
                }
            }
            return Ok(colors);
        }

        ////////////////////////////////////////////////////////
        ///
        //all price in category
        //  api/Categories/allcategoryprice/1
        [HttpGet("{id}")]

        [Route("allcategoryprice/{id}")]
        public async Task<IActionResult> getallpriceincategory(int id)
        {
            //id is category id

            List<object> prices = new List<object>();
            var sub = await _context.SubCategories.Where(a => a.CategoryId == id).ToListAsync();
            if (sub == null)
            {
                return NotFound("wrong id");
            }


            foreach (var item in sub)
            {
                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).ToListAsync();
                foreach (var pro in products)
                {
                    if (!prices.Contains(pro.Price))
                    {
                        if (pro.Price != 0)
                        {
                            prices.Add(pro.Price);
                        }
                    }
                }
            }
            return Ok(prices);
        }

        //all discount in category
        //  api/Categories/allcategorydiscount/1
        [HttpGet("{id}")]

        [Route("allcategorydiscount/{id}")]
        public async Task<IActionResult> getalldiscountincategory(int id)
        {
            //id is category id
            List<object> discount = new List<object>();
            var sub = await _context.SubCategories.Where(a => a.CategoryId == id).ToListAsync();
            if (sub == null)
            {
                return NotFound("wrong id");
            }


            foreach (var item in sub)
            {
                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).ToListAsync();
                foreach (var pro in products)
                {
                    if (!discount.Contains(pro.Discount))
                    {
                        if (pro.Discount != null)
                        {
                            discount.Add(pro.Discount);
                        }
                    }
                }
            }
            return Ok(discount);
        }


        /////////////////////////////////////////////////////////
        ///

        //all rating in category
        //  api/Categories/allcategoryrating/1
        [HttpGet("{id}")]

        [Route("allcategoryrating/{id}")]
        public async Task<IActionResult> getallratingincategory(int id)
        {
            //id is category id
            List<object> rating = new List<object>();
            var sub = await _context.SubCategories.Where(a => a.CategoryId == id).ToListAsync();
            if (sub == null)
            {
                return NotFound("wrong id");
            }


            foreach (var item in sub)
            {
                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).ToListAsync();
                foreach (var pro in products)
                {
                    if (!rating.Contains(pro.Rating))
                    {
                        if (pro.Rating != null)
                        {
                            rating.Add(pro.Rating);
                        }
                    }
                }
            }
            return Ok(rating);
        }
        ////////////////////////////////////////////////////



        ///////////////////////////////////////////////////


        //get all product in category

        //   api/Categoriesapi/allproductonlyIncategory/1

        [HttpGet("{id}")]
        [Route("allproductonlyIncategory/{id}")]
        public async Task<ActionResult> GetallproductonlyIncategory(int id)
        {//id is category id
            List<object> allproductonly = new List<object>();

            var sub = await _context.SubCategories.Where(a => a.CategoryId == id).ToListAsync();
            foreach (var item in sub)
            {
                var products = await _context.Products.Where(a => a.SubcategoryId == item.SubcategoryId).ToArrayAsync();
                if (products != null) { 
                    foreach(var oitem in products) { 
                if (!allproductonly.Contains(oitem))
                {
                    if (oitem != null)
                    {
                        allproductonly.Add(oitem);
                    }

                }
            }
                }
            }
            return Ok(allproductonly);

        }

    }
}
