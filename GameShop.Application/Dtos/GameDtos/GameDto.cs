using GameShop.Domain.Enums;

namespace GameShop.Application.Dtos.GameDtos
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int StockLevel { get; set; }
        public decimal Price { get; set; }
        public Platform Platform { get; set; }
    }
}