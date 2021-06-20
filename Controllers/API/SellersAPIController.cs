using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using J6.DAL.Entities;
using J6.Models;
using J6.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace J6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersAPIController : ControllerBase
    {
        private readonly DbContainer _context;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public SellersAPIController(UserManager<AppUser> userManager, IMapper mapper, DbContainer context)
        {
            _context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        //getAllSeller
        //api/Sellers
        [HttpGet]
        public async Task<ActionResult<List<SellerDto>>> getAllSeller()
        {
            var Sellers = await userManager.GetUsersInRoleAsync("Seller");
            var SellersToReturn = mapper.Map<List<SellerDto>>(Sellers);
            return Ok(SellersToReturn);
        }


        //getUserById
        //api/Sellers/1
        [HttpGet("{id}")]
        public async Task<ActionResult<SellerDto>> GetSellerById(int id)
        {
            var Sellers = await userManager.GetUsersInRoleAsync("Seller");
            var Seller = Sellers.SingleOrDefault(S => S.Id == id);
            if (Seller == null) return NotFound("No Seller Matched");

            var SellerToRetuen = mapper.Map<SellerDto>(Seller);

            return Ok(SellerToRetuen);
        }
        //////////////////////////////////////////////////////////////////////
        //seller add product
        [HttpPost("{id}")]
        public async Task<ActionResult> sellerAddProduct(Product prod)
        {

            _context.Products.Add(prod);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(prod.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = prod.Id }, prod);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        ///////////////////////////////////////////////////////////////
        //get all seller products
        // api/SellersAPI/sellerproducts/1
        [HttpGet("{id}")]
        [Route("sellerproducts/{id}")]
        public async Task<ActionResult> GetSellerProduct(int id)
        {
            //seller id
            var Sellers = await userManager.GetUsersInRoleAsync("Seller");
            var Seller = Sellers.SingleOrDefault(S => S.Id == id);
            if (Seller == null) return NotFound("No Seller Matched");

            var product = await _context.Products.Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(o => o.ProductImages).Include(i => i.Reviews).Include(y => y.Views).FirstOrDefaultAsync(q => q.SellerId == Seller.Id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        ////////////////////////////////////////////////////
        // seller edit in his product

        [HttpPut("{id}/{sellerid}")]
        public async Task<IActionResult> PutSellerProduct(int id, Product product, int sellerid)
        {// id is product id
            if (id != product.Id)
            {
                return BadRequest();
            }
            if (sellerid != product.SellerId)
            {
                return BadRequest("not allawed to use to edit this product you aren't the seller");
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

    }

}
