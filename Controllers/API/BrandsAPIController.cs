using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsAPIController : ControllerBase
    {

        private readonly DbContainer _context;

        public BrandsAPIController(DbContainer context)
        {
            _context = context;
        }




        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
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

        ////////////////////////////////////////////////////////
        ///
        //all color in brand
        //  api/Brands/allbrandprice/1
        [HttpGet("{id}")]

        [Route("allbrandprice/{id}")]
        public async Task<IActionResult> getpriceInbrand(int id)
        {
            //id is brand id
            List<object> prices = new List<object>();
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound("wrong id");
            }


            var products = await _context.Products.Where(a => a.BrandId == brand.BrandId).ToListAsync();

            foreach (var item in products)
            {
                if (!prices.Contains(item.Price))
                {
                    if (item.Price != null)
                    {
                        prices.Add(item.Price);
                    }
                }
            }
            return Ok(prices);

        }

        ////////////////////////////////////////////////////////
        ///
        //all color in brand
        //  api/Brands/allbrandcolor/1
        [HttpGet("{id}")]

        [Route("allbrandcolor/{id}")]
        public async Task<IActionResult> getcolorInbrand(int id)
        {
            //id is brand id
            List<string> colors = new List<string>();
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound("wrong id");
            }


            var products = await _context.Products.Where(a => a.BrandId == brand.BrandId).ToListAsync();

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
        //////////////////////////////////////////////////////
        /// //all discount in brand
        //  api/Brands/allbranddiscount/1
        [HttpGet("{id}")]

        [Route("allbranddiscount/{id}")]
        public async Task<IActionResult> getdiscountInbrand(int id)
        {
            //id is brand id
            List<object> discount = new List<object>();


            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound("wrong id");
            }


            var products = await _context.Products.Where(a => a.BrandId == brand.BrandId).ToListAsync();

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


        //////////////////////
        ///

        //all rating in brand
        //  api/Brands/allbrandrating/1
        [HttpGet("{id}")]

        [Route("allbrandrating/{id}")]
        public async Task<IActionResult> getratingInbrands(int id)
        {
            //id is brand id
            List<object> rating = new List<object>();


            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound("wrong id");
            }


            var products = await _context.Products.Where(a => a.BrandId == brand.BrandId).ToListAsync();

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


