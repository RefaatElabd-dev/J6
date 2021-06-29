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
using J6.BL.Servises;

namespace J6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersAPIController : ControllerBase
    {
        private readonly DbContainer _context;
        private readonly ITokenServices _tokenService;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public SellersAPIController(UserManager<AppUser> userManager, IMapper mapper, DbContainer context, ITokenServices tokenService)
        {
            _context = context;
            _tokenService = tokenService;
            this.userManager = userManager;
            this.mapper = mapper;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExist(registerDto.Username)) { return BadRequest("User is taken"); }

            AppUser user = new AppUser
            {
                UserName = registerDto.Username,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            user.IsActive = false;
            await userManager.UpdateAsync(user);
            await userManager.AddToRoleAsync(user, "Seller");
            return new UserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email,
                Id = user.Id,
            };
        }

        private async Task<bool> UserExist(string username)
        {
            return await userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
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
        [HttpPost]
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
            var product = await _context.Products.Where(q => q.SellerId == Seller.Id).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(i => i.Reviews).Include(y => y.Views).ToListAsync();
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        ////////////////////////////////////////////////////
        // seller edit in his product
        // api/SellersAPI/1/8
        [HttpPut("{id}/{sellerid}")]
        public async Task<IActionResult> PutSellerProduct(int id, Product product, int sellerid)
        {// id is product id
            if (id != product.Id||sellerid !=product.SellerId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            return NoContent();
        }
        ///////////////////////////////////////
        ///get allseller with their products
        /// api/SellersAPI/AllsellerAllproducts
        [HttpGet]
        [Route("AllsellerAllproducts")]
        public async Task<ActionResult> GetallSellersandProducts()
        {
            List<object> all =new List<object>();
            var Sellers = await userManager.GetUsersInRoleAsync("Seller");
            if (Sellers == null) return NotFound("No Seller exist");
            foreach(var item in Sellers)
            {
                var prod =await _context.Products.Where(a=>a.SellerId==item.Id).ToListAsync();
                foreach(var i in prod)
                {
                    all.Add(i);
                }
               
            }

            return Ok(all);
        }
        ///////////////////////////////////////////////////////////
        ///edit discount using seller
        ///  api/SellersAPI/editDiscountByseller
        [HttpPut("{id}/{sellerid}")]
        [Route("editDiscountByseller/{id}/{sellerid}")]
        public async Task<IActionResult> PutByproductdicount(int id,[FromBody] double discount, int sellerid)
        {// id is product id
            var product = await _context.Products.FirstOrDefaultAsync(a => a.Id == id && a.SellerId == sellerid);
            product.Discount=discount;
            await _context.SaveChangesAsync();
            return NoContent();
        }






        /////////////////////////////////////////////////////////////
    }

}
