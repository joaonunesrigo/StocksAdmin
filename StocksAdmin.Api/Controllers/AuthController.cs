using Microsoft.AspNetCore.Mvc;
using StocksAdmin.Api.Services.User;
using StocksAdmin.Communication.Requests.User;

namespace StocksAdmin.Api.Controllers
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

            long userId = _userService.ValidateLogin(userLoginRequest);

            if (userId == 0)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return Unauthorized("Usuário não encontrado.");
            }

            var token = JwtTokenService.GenerateToken(userLoginRequest.Email, userId);

            return Ok(new { Token = token, UserId = userId });
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