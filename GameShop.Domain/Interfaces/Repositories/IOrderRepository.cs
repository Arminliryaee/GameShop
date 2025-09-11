using GameShop.Domain.Entities;
using GameShop.Domain.Interfaces.Common;

namespace GameShop.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
    }
}
