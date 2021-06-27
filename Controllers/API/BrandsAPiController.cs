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
    public class BrandsAPiController : ControllerBase
    {
        private readonly DbContainer _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public BrandsAPiController(DbContainer context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;

        }
        //https://localhost:44340/api/Brandsapi
        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            return await _context.Brands
                .Select(x => new Brand()
                {
                    BrandId = x.BrandId,
                    BrandName = x.BrandName,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Image = "images/" + x.Image,
                }).ToListAsync();

        }


  //https://localhost:44340/api/Brandsapi/3

        // GET: api/Brands/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        // PUT: api/Brands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.BrandId)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        // POST: api/Brands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new { id = brand.BrandId }, brand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.BrandId == id);
        }

        ///////////////////////////////////////////////////////////////////////////////////
        //get all color in specific  brand
        // api/BrandsAPi/allcolorinBRAND/1
        [HttpGet("{id}")]
        [Route("allcolorinBRAND/{id}")]
        public async Task<ActionResult> GetColorsInBrand(int id)
        {
            //id is brand id
            List<string> colors = new List<string>();
            var brand = await _context.Brands.FirstOrDefaultAsync(a=>a.BrandId==id);

            if (brand == null)
            {
                return NotFound();
            }
                var products = await _context.Products.Where(a =>a.BrandId==brand.BrandId ).ToListAsync();
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


            return Ok(colors);
        }
        ///////////////////////////////////////////////
        //all price in specific brand
        //  api/BrandsAPi/AllPriceinBRAND/1
        [HttpGet("{id}")]
        [Route("AllPriceinBRAND/{id}")]
        public async Task<IActionResult> getallpriceinBRAND(int id)
        {
            //id is brand id

            List<object> prices = new List<object>();
            var brand = await _context.Brands.FirstOrDefaultAsync(a => a.BrandId == id);

            if (brand == null)
            {
                return NotFound();
            }
            var products = await _context.Products.Where(a => a.BrandId == brand.BrandId).ToListAsync();
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


            return Ok(prices);
        }
        //////////////////////////////////////////////////////////////////////
        //all discount in specific brand
        //  api/BrandsAPi/allBRANDdiscount/1
        [HttpGet("{id}")]

        [Route("allBRANDdiscount/{id}")]
        public async Task<IActionResult> getalldiscountinBRAND(int id)
        {
            //id is category id
            List<object> discount = new List<object>();
            var brand = await _context.Brands.FirstOrDefaultAsync(a => a.BrandId == id);

            if (brand == null)
            {
                return NotFound();
            }
            var products = await _context.Products.Where(a => a.BrandId == brand.BrandId).ToListAsync();

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
            
            return Ok(discount);
        }
        //////////////////////////////////////////////////////////////////////
        //all rating in category
        //  api/BrandsAPi/allBRANDrating/1
        [HttpGet("{id}")]

        [Route("allBRANDrating/{id}")]
        public async Task<IActionResult> getallratinginBRAND(int id)
        {
            //id is category id
            List<object> rating = new List<object>();
            var brand = await _context.Brands.FirstOrDefaultAsync(a => a.BrandId == id);

            if (brand == null)
            {
                return NotFound();
            }
            var products = await _context.Products.Where(a => a.BrandId == brand.BrandId).ToListAsync();

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
            
            return Ok(rating);
        }

        //////////////////////////////////////////////////////////////////////////////////

    }
}
