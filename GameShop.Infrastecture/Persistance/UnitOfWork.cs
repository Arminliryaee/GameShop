using GameShop.Domain.Interfaces;
using GameShop.Domain.Interfaces.Repositories;
using GameShop.Infrastecture.Persistance.Repositories;
using GameShop.Infrastructure.Persistence.Repositories;

namespace GameShop.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGameRepository Games { get; private set; }
        public IUserRepository Users { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Games = new GameRepository(_context);
            Users = new UserRepository(_context);
            Orders = new OrderRepository(_context);
            ShoppingCarts = new ShoppingCartRepository(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}