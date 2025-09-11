using GameShop.Domain.Entities;
using GameShop.Domain.Interfaces.Common;

namespace GameShop.Domain.Interfaces.Repositories
{
    public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetByUserIdAsync(Guid userId);
    }
}
