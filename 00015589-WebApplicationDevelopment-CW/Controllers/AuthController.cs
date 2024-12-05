using _00015589_WebApplicationDevelopment_CW.Dtos;
using _00015589_WebApplicationDevelopment_CW.Services;
using Microsoft.AspNetCore.Mvc;

namespace _00015589_WebApplicationDevelopment_CW.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                await _userService.RegisterUserAsync(userDto);
                return Ok(new { message = "User registered successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var loginRes = await _userService.LoginUserAsync(loginDto);
                return Ok(new { loginRes.Token, loginRes.User });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
