using MyApi.Data;
using MyApi.Models;
using MyApi.Interface;
using Microsoft.EntityFrameworkCore;
using MyApi.Dtos.Stock;

namespace MyApi.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Stocks> CreateStockAsync(Stocks stock)
        {
            await _context.AddAsync(stock);
            await SaveChangesAsync();
            return stock;
        }

        public async Task<bool> DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return false;

            _context.Stocks.Remove(stock);
            await SaveChangesAsync();

            return true;
        }


        public  async Task<List<Stocks>> GetAllStocksAsync()
        {
            return await _context.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stocks?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Stocks?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null) return null;

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = (long)stockDto.MarketCap;

            await SaveChangesAsync();

            return existingStock;
        }
    
    }
}