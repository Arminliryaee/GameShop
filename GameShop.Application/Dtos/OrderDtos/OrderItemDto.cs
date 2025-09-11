namespace GameShop.Application.Dtos.OrderDtos
{
    public class OrderItemDto
    {
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}