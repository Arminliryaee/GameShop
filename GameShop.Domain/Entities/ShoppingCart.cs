namespace GameShop.Domain.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<CartItem> Items { get; set; } = [];
    }
}