using GameShop.Domain.Enums;

namespace GameShop.Domain.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; } = 0;
        public decimal Price { get; set; }
        public Platform Platform { get; set; }
    }
}
