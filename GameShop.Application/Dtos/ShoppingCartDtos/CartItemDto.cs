namespace GameShop.Application.Dtos.ShoppingCartDtos
{
    public class CartItemDto
    {
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}