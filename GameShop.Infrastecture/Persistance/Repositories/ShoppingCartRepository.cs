using GameShop.Domain.Entities;
using GameShop.Domain.Interfaces.Repositories;
using GameShop.Infrastructure.Persistence;
using GameShop.Infrastructure.Persistence.Repositories;

namespace GameShop.Infrastecture.Persistance.Repositories
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
