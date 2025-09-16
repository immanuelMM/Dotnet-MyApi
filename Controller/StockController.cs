using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Mappers;
using MyApi.Dtos.Stock;
using Microsoft.EntityFrameworkCore;
using MyApi.Interface;
using MyApi.Repository;

namespace MyApi.Controller
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var stocks = await _stockRepository.GetAllStocksAsync();
            var stockDtos = stocks.Select(s => s.ToStockDto());
            return Ok(stockDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var stock = await _stockRepository.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepository.CreateStockAsync(stockModel);

            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var existingStock = await _stockRepository.UpdateStockAsync(id, stockDto);

            if (existingStock == null)
            {
                return NotFound();
            }

            return Ok(existingStock.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var stockDeleted = await _stockRepository.DeleteStockAsync(id);

            if (!stockDeleted)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}