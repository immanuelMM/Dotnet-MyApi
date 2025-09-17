using MyApi.Dtos.Stock;
using MyApi.Helper;
using MyApi.Models;

namespace MyApi.Interface
{
    public interface IStockRepository
    {
        Task<List<Stocks>> GetAllStocksAsync(QueryObject query);
        Task<Stocks?> GetStockByIdAsync(int id);
        Task<Stocks> CreateStockAsync(Stocks stock);
        Task<Stocks?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto);
        Task<bool> SaveChangesAsync();
        Task<bool> DeleteStockAsync(int id);
    }
}