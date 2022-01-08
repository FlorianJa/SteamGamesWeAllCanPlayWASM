using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SteamGamesWeAllCanPlayWASM.Data.Repositories;
using System.Security.Claims;

namespace SteamGamesWeAllCanPlayWASM.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMeUserAsync()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return Ok(await _userRepo.GetByIdAsync(int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value)));
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }


        [ResponseCache(NoStore = true, Duration = 0)]
        [HttpGet("~/signin"), HttpPost("~/signin")]
        public IActionResult SignIn()
        {
            string returnUrl = "/";
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = returnUrl,
                IsPersistent = true,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, "Steam");
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        [HttpGet("~/signout"), HttpPost("~/signout")]
        public IActionResult LogOut()
        {
            string returnUrl = "/";
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = returnUrl,
            }, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
