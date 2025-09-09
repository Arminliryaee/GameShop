using GameShop.Domain.Enums;

namespace GameShop.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal TotalValue { get; set; }
        public OrderState State { get; set; }
    }
}
