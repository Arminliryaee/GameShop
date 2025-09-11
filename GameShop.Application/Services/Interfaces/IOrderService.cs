using GameShop.Application.Dtos.OrderDtos;

namespace GameShop.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderFromCartAsync(Guid userId);
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(Guid userId);
        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
        Task<bool> CancelOrderAsync(Guid orderId);
    }
}