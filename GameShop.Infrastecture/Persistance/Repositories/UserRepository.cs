using GameShop.Domain.Entities;
using GameShop.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username) => await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
    }
}