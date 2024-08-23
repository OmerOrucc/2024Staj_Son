using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserAuthService.Models;
using UserAuthService.Services;

namespace UserAuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public UserController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

         [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User
            {
                kullaniciMail = model.kullaniciMail,
                kullaniciSifre = model.kullaniciSifre,
                kullaniciAdi = model.kullaniciAdi,
                kullaniciSoyadi = model.kullaniciSoyadi,
                kullaniciAdres = model.kullaniciAdres
            };

            var result = await _userService.RegisterUserAsync(user, model.kullaniciSifre);
            if (result)
            {
                return Ok(new { message = "User registered successfully" });
            }
            else
            {
                return StatusCode(500, "An error occurred while registering the user");
            }
        }

        [HttpPost("login")]
        public  String Login([FromBody] LoginModel model)
        {
            var user =  _context.Users.SingleOrDefault(u => u.kullaniciMail == model.kullaniciMail && u.kullaniciSifre == model.kullaniciSifre);
              if (user != null)
                return "Giris Basarili";
            return "Invalid login attempt";
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser([FromQuery] string userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            if (result)
                return Ok("User deletion successful");
            return BadRequest("User deletion failed");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var result = await _userService.ResetPasswordAsync(model.kullaniciMail, model.kullaniciYeniSifre);
            if (result)
                return Ok("Password reset successful");
            return BadRequest("Password reset failed");
        }
    }
}
