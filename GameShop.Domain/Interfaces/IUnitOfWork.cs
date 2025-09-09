using GameShop.Domain.Interfaces.Repositories;

namespace GameShop.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository GameRepository { get; }
        IUserRepository UserRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IOrderRepository OrderRepository { get; }
    }
}
