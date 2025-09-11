using GameShop.Domain.Entities;
using GameShop.Domain.Interfaces.Repositories;

namespace GameShop.Infrastructure.Persistence.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}