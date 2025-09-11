using GameShop.Domain.Entities;
using GameShop.Domain.Interfaces.Repositories;
using GameShop.Infrastructure.Persistence;
using GameShop.Infrastructure.Persistence.Repositories;

namespace GameShop.Infrastecture.Persistance.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
