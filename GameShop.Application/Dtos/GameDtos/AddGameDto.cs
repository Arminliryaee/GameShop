using GameShop.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GameShop.Application.Dtos.GameDtos
{
    public class AddGameDto
    {
        [Required(ErrorMessage = "Game Name is requierd!")]
        [StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        [Range(0, int.MaxValue , ErrorMessage = "Stock cannot be negetive")]
        public int StockLevel { get; set; } = 0;
        [Required(ErrorMessage = "Price is required!")]
        [Range(0, 1000)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Platform is requierd!")]
        public Platform Platform { get; set; }
    }
}
