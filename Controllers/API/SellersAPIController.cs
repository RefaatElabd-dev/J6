using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using J6.DAL.Entities;
using J6.Models;

namespace J6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersAPIController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public SellersAPIController(UserManager<AppUser> userManager, IMapper mapper)
        {
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
            //var user = await userManager.FindByIdAsync(id.ToString());
            //if (user == null) return NotFound("This Password Is Not Exist");

            var Sellers = await userManager.GetUsersInRoleAsync("Seller");
            var Seller = Sellers.SingleOrDefault(S => S.Id == id);
            if (Seller == null) return NotFound("No Seller Matched");

            var SellerToRetuen = mapper.Map<SellerDto>(Seller);

            return Ok(SellerToRetuen);
        }




        ////editsellerdata
        //[HttpPut("{id}")]
        //public async Task<ActionResult<User>> PutSeller(int id, [FromBody] User user)
        //{


        //    if (!SellerExists(id)) { return NotFound(); }
        //    if (!UserExists(id))
        //    {
        //        return NotFound();
        //    }
        //    var seller = await _jumia1Context.Sellers.FirstOrDefaultAsync(s => s.Id == id);
        //    var userSeller = await _jumia1Context.Users.FirstOrDefaultAsync(s => s.Userid == seller.Id);
        //    userSeller.FirstName = user.FirstName;
        //    userSeller.LastName = user.LastName;
        //    userSeller.Gender = user.Gender;
        //    userSeller.Email = user.Email;
        //    userSeller.Password = user.Password;
        //    // userSeller.RoleId = user.RoleId;
        //    userSeller.UserAddresses = user.UserAddresses;

        //    await _jumia1Context.SaveChangesAsync();
        //    return userSeller;
        //}






        ////delete  
        ////api/Sellers/3
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSeller(int id)
        //{
        //    var seller = await _jumia1Context.Sellers.FindAsync(id);
        //    var sellerInUser = await _jumia1Context.Users.FindAsync(seller.Id);
        //    if (seller == null)
        //    {
        //        return NotFound();
        //    }

        //    _jumia1Context.Sellers.Remove(seller);
        //    _jumia1Context.Users.Remove(sellerInUser);
        //    await _jumia1Context.SaveChangesAsync();

        //    return NoContent();
        //}


        //check if seller exist

        //private bool SellerExists(int id)
        //{
        //    return _jumia1Context.Sellers.Any(e => e.Id == id);
        //}
        ////check if user exsist
        //private bool UserExists(int id)
        //{
        //    return _jumia1Context.Users.Any(e => e.Userid == id);
        //}





    }

}
