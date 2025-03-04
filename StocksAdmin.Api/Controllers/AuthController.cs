using Microsoft.AspNetCore.Mvc;
using ProductClientHub.Api.Services.User;
using ProjectClientHub.Communication.Requests.User;

namespace ProductClientHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest userLoginRequest)
        {
            if (userLoginRequest == null || string.IsNullOrEmpty(userLoginRequest.Email) || string.IsNullOrEmpty(userLoginRequest.Password))
            {
                return BadRequest("Credenciais são obrigatórias.");
            }

            bool isValidUser = _userService.ValidateLogin(userLoginRequest);

            if (!isValidUser)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            var token = JwtTokenService.GenerateToken(userLoginRequest);

            return Ok(new { Token = token });
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            try
            {
                var response = await _userService.Register(userRegisterRequest);

                return Created("", response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}