using GameShop.Application.Dtos.ShoppingCartDtos;

namespace GameShop.Application.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetCartByUserIdAsync(Guid userId);
        Task<ShoppingCartDto> AddItemToCartAsync(Guid userId, AddItemToCartDto itemDto);
        Task<bool> RemoveItemFromCartAsync(Guid userId, Guid gameId);
        Task<bool> ClearCartAsync(Guid userId);
    }
}