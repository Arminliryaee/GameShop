using GameShop.Domain.Entities;

namespace GameShop.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}