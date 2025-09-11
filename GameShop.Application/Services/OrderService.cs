using GameShop.Application.Dtos.OrderDtos;
using GameShop.Application.Services.Interfaces;
using GameShop.Domain.Entities;
using GameShop.Domain.Enums;
using GameShop.Domain.Interfaces;

namespace GameShop.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        

        public async Task<OrderDto> CreateOrderFromCartAsync(Guid userId)
        {
            var cart = await _unitOfWork.ShoppingCarts.GetByUserIdAsync(userId);
            if (cart == null || !cart.Items.Any())
                throw new Exception("Shopping cart is empty.");
            

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                State = OrderState.Pending, 
                Items = []
            };

            decimal totalValue = 0;

            foreach (var cartItem in cart.Items)
            {
                var game = await _unitOfWork.Games.GetByIdAsync(cartItem.GameId);
                if (game == null || game.Stock < cartItem.Quantity)
                    throw new Exception($"Not enough stock for game: {game?.Name}");
                

                game.Stock -= cartItem.Quantity;
                _unitOfWork.Games.Update(game);

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    GameId = cartItem.GameId,
                    Quantity = cartItem.Quantity,
                    Price = game.Price
                };

                order.Items.Add(orderItem);
                totalValue += orderItem.Price * orderItem.Quantity;
            }

            order.TotalValue = totalValue;

            await _unitOfWork.Orders.AddAsync(order);

            cart.Items.Clear();
            _unitOfWork.ShoppingCarts.Update(cart);

            await _unitOfWork.CompleteAsync();

            return MapOrderToDto(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId);
            return orders.Select(MapOrderToDto);
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            return order != null ? MapOrderToDto(order) : null;
        }

        public async Task<bool> CancelOrderAsync(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null || order.State == OrderState.Completed)
                return false;
            
            order.State = OrderState.Cancelled;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        private OrderDto MapOrderToDto(Order order) => new()
        {
            Id = order.Id,
            OrderState = order.State.ToString(),
            TotalValue = order.TotalValue,
            OrderItems = [.. order.Items.Select(oi => new OrderItemDto
                {
                    GameName = oi.Game?.Name,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                })]
        };
    }

}