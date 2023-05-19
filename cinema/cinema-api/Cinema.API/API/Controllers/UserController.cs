
using Business.DTOs;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IShowingService _showingService;

        public UserController(IUserService userService, IShowingService showingService)
        {
            _userService = userService;
            _showingService = showingService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerData)
        {
            var user = await _userService.Register(registerData);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginData)
        {
            var user = await _userService.Login(loginData);

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("reloadData")]
        public async Task<IActionResult> ReloadData()
        {
            await _showingService.ReloadShowingsAsync();

            return Ok();
        }
    }
}
