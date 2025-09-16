using MyApi.Dtos.Stock;
using MyApi.Models;

namespace MyApi.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stocks stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(s => s.ToCommentDto()).ToList()
            };
        }

        public static Stocks ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stocks
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = (long)stockDto.MarketCap
            };
        }

    }
}