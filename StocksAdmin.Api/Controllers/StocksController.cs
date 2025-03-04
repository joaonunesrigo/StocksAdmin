using Microsoft.AspNetCore.Mvc;
using StocksAdmin.Api.Services.Stocks;
using StocksAdmin.Communication.Requests.Stock;
using StocksAdmin.Communication.Responses.Stocks;

namespace StocksAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly StockService _stockService;

        public StocksController(StockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(StockResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] StockRequest stockRequest)
        {
            try
            {
                var response = await _stockService.CreateStock(stockRequest);

                return Created("", response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] long id, [FromBody] StockRequest stockRequest)
        {
            try
            {
                StockResponse stockResponse = _stockService.UpdateStock(id, stockRequest);
                return Ok(stockResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _stockService.GetAllStocks();

            if (response.Stocks.Count == 0) return NoContent();

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
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
