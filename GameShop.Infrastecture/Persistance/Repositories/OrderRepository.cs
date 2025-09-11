using GameShop.Domain.Entities;
using GameShop.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Game)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}