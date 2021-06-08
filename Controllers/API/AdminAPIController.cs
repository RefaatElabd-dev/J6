using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using J6.DAL.Entities;

namespace J6.Controllers
{
    public class AdminAPiController : ControllerBase 
    {
        private readonly UserManager<AppUser> userManager;

        public AdminAPiController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize(Policy ="RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public IActionResult GetUsersWithRoles()
        {
            var users = userManager.Users
                .Include(u => u.userRoles)
                .ThenInclude(ur => ur.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    Name = u.UserName,
                    Roles = u.userRoles.Select(ur => ur.Role.Name)
                });
            return Ok(users);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost("Edit-roles/{UserName}")]
        public async Task<ActionResult> EditRoles(string UserName, [FromQuery] string roles)
        {
            var user = await userManager.FindByNameAsync(UserName.ToLower());
            if (user == null) return NotFound("Could not find user");

            var selectedRoles = roles.Split(',').ToArray();

            var userRoles = await userManager.GetRolesAsync(user);

            var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
            if (!result.Succeeded) return BadRequest("Failed Yo Add To Roles");

            result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if (!result.Succeeded) return BadRequest("Failed to Remove Roles");

            return Ok(await userManager.GetRolesAsync(user));

        }
    }
}
