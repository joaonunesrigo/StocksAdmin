using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StocksAdmin.Api.Services.User;
using StocksAdmin.Api.Services.Wallet;
using StocksAdmin.Communication.Requests.Wallet;
using StocksAdmin.Communication.Responses.Wallets;

namespace StocksAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly WalletService _walletService;

        public WalletController(WalletService walletService, UserService userService)
        {
            _walletService = walletService;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] WalletRequest walletRequest)
        {
            var userId = _userService.GetIdUserAuthenticated(HttpContext.User);

            WalletResponse walletResponse = _walletService.CreateWallet(walletRequest, userId);
            return Created("", walletResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] long id, [FromBody] WalletRequest walletRequest)
        {
            try
            {
                WalletResponse walletResponse = _walletService.UpdateWallet(id, walletRequest);
                return Ok(walletResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userId = _userService.GetIdUserAuthenticated(HttpContext.User);

            var response = _walletService.GetAllUserWallets(userId);

            if (response.Wallets.Count == 0) return NoContent();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
