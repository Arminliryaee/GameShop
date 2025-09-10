using GameShop.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GameShop.Application.Dtos.GameDtos
{
    public class UpdateGameDto
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public int StockLevel { get; set; }
        [Range(0, 1000)]
        public decimal Price { get; set; }
        public Platform Platform { get; set; }
    }
}
