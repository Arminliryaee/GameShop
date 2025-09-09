namespace GameShop.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public int Quantity { get; set; } 
        public decimal Price { get; set; }
    }
}