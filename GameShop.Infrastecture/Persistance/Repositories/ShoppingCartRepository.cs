using GameShop.Domain.Entities;
using GameShop.Domain.Interfaces.Repositories;
using GameShop.Infrastructure.Persistence;
using GameShop.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastecture.Persistance.Repositories
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<ShoppingCart> GetByUserIdAsync(Guid userId) => 
            await _dbSet
                .Include(s => s.Items) 
                .ThenInclude(i => i.Game)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        
    }
}
