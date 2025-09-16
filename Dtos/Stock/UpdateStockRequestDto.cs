using System.ComponentModel.DataAnnotations;
namespace MyApi.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "synbol max lengt is 10 Char")]
        public string Symbol { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "CompanyName max lengt is 10 Char")]
        public string CompanyName { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry max lengt is 10 Char")]
        public string Industry { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal MarketCap { get; set; }
    }
}