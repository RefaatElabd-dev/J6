using J6.BL.Servises;
using J6.DAL.Entities;
using J6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace J6.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AccountAPiController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenServices tokenService;
        public AccountAPiController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenServices _tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            tokenService = _tokenService;
        }
<<<<<<< HEAD
        [HttpPost("Register")]
=======
        [HttpPost("Regester")]
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
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
            var result = await  userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return new UserDto
            {
                UserName = user.UserName,
<<<<<<< HEAD
                Token = await tokenService.CreateToken(user)
=======
                Token = await tokenService.CreateToken(user),
                Email = user.Email,

              
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
            };
        }

        private async Task<bool> UserExist(string username)
        {
            return await userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        [HttpPost("Login")]
<<<<<<< HEAD
        public async Task<ActionResult<UserDto>> Login(LoginDto LoginDto)
=======
        public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto LoginDto)
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.UserName == LoginDto.UserName.ToLower());
            if (user == null) return Unauthorized("This UserName is not Exist");

            var result = await signInManager.CheckPasswordSignInAsync(user, LoginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid Password");
            return new UserDto
            {
                UserName = user.UserName,
<<<<<<< HEAD
                Token = await tokenService.CreateToken(user)
=======
                Token = await tokenService.CreateToken(user),
                Email = user.Email,
                Id=user.Id
                
                
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
            };
        }
    }
}
