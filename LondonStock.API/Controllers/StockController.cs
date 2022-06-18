using LondonStock.API.Business.Services;
using LondonStock.API.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LondonStock.API.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("stocks/tickerSymbol")]
        public async Task<IActionResult> GetStock(string tickerSymbol)
        {
            if (string.IsNullOrWhiteSpace(tickerSymbol))
            {
                return BadRequest();
            }

            var stock = await _stockService.GetStockAsync(tickerSymbol);

            if (stock != null)
            {
                return Ok(stock);
            }

            return NoContent();
        }

        [HttpGet("stocks")]
        public async Task<IActionResult> GetStock()
        {
            var stock = await _stockService.GetStockAsync();

            if (stock.Any())
            {
                return Ok(stock);
            }

            return NoContent();
        }

        [HttpPost("stocks/tickerSymbols")]
        public async Task<IActionResult> GetStock([FromBody] List<string> tickerSymbols)
        {
            if (!tickerSymbols.Any())
            {
                return BadRequest();
            }

            var stock = await _stockService.GetStockAsync(tickerSymbols);

            if (stock.Any())
            {
                return Ok(stock);
            }

            return NoContent();
        }

        [HttpPost("stocks/AddTransaction/{tickerSymbol}")]
        public async Task<IActionResult> AddStock(string tickerSymbol, [FromBody] StockRequest stockRequest)
        {
            await _stockService.AddStockAsync(tickerSymbol, stockRequest);
            return Ok();
        }
    }
}
