namespace GameShop.Application.Dtos.OrderDtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderState { get; set; }
        public decimal TotalValue { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = [];
    }
}