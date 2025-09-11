namespace GameShop.Application.Dtos.ShoppingCartDtos
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItemDto> Items { get; set; } = [];
        public decimal TotalPrice { get; set; }
    }
}