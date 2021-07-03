﻿using J6.BL.Servises;
using J6.DAL.Entities;
using J6.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBagApiController : Controller
    {
        private readonly IUserSavedBagServices _userSavedBag;

        public UserBagApiController(IUserSavedBagServices userSavedBag)
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

        [HttpGet]
        [Route("IsSaved/{UserId}/{ProductId}")]
        public async Task<ActionResult<bool>> IsSaved(int UserId, int ProductId)
        {
            return Ok(await _userSavedBag.IsSaved(UserId, ProductId));
        }

        [HttpDelete("DeleteSavedItem/{UserId}/{ProductId}")]
        public async Task<ActionResult> DeleteSavedItem(int UserId, int ProductId)
        {
            return await _userSavedBag.DeleteSavedItemAsync(UserId, ProductId) ?
                                                NoContent() : BadRequest("Check User and Product ");
        }
    }
}
