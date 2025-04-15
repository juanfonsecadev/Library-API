using Microsoft.AspNetCore.Mvc;
using Library.API.Models;
using System.Threading.Tasks;
using Library.API.Dtos;
using Library.API.Services;


namespace Library.API.Controllers
{
    [ApiController]
    [Route("authorization")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var response = await _authService.RegisterAsync(request);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var response = await _authService.LoginAsync(request);
            if (!response.Success)
                return Unauthorized(response.Message);

            return Ok(response);
        }
    }
}
