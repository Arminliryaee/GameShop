using GameShop.Domain.Interfaces.Repositories;

namespace GameShop.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Games { get; }
        IUserRepository Users { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        IOrderRepository Orders { get; }

        Task<int> CompleteAsync();
    }
}