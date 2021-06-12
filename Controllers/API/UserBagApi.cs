using J6.BL.Servises;
using J6.DAL.Entities;
using J6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserBagApi : ControllerBase
    {
        private readonly IUserSavedBagServices _userSavedBag;

        public UserBagApi(IUserSavedBagServices userSavedBag)
        {
            _userSavedBag = userSavedBag;
        }

        [HttpPost]
        [Route("SetProductToSavedItems")]
        public async Task<IActionResult> SetProductToSavedItems([FromBody] BagDto bag)
        {
            await _userSavedBag.SetItemToSavedBagAsync(bag.UserId, bag.ProductId);
            return NoContent();
        }

        [HttpGet]
        [Route("GetProductFromSavedItems/{UserId}")]
        public async Task<ActionResult<ICollection<Product>>> GetProductFromSavedItems(int UserId)
        {
            return Ok(await _userSavedBag.GetSavedProductsAsync(UserId));
        }


    }
}
