using GameShop.Application.Dtos.ShoppingCartDtos;
using GameShop.Application.Services.Interfaces;
using GameShop.Domain.Entities;
using GameShop.Domain.Interfaces;

namespace GameShop.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


        public async Task<ShoppingCartDto> AddItemToCartAsync(Guid userId, AddItemToCartDto itemDto)
        {
            var cart = await GetOrCreateCartByUserIdAsync(userId);
            var game = await _unitOfWork.Games.GetByIdAsync(itemDto.GameId);

            if (game == null || game.IsDeleted)
                throw new Exception("Game not found.");

            if (game.Stock < itemDto.Quantity)
                throw new Exception("Not enough stock available.");

            var cartItem = cart.Items.FirstOrDefault(i => i.GameId == itemDto.GameId);

            if (cartItem != null)
                cartItem.Quantity += itemDto.Quantity;
            else
            {
                cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    GameId = itemDto.GameId,
                    Quantity = itemDto.Quantity,
                    ShoppingCartId = cart.Id
                };
                cart.Items.Add(cartItem);
            }

            await _unitOfWork.CompleteAsync();
            return await GetCartByUserIdAsync(userId);
        }

        public async Task<bool> ClearCartAsync(Guid userId)
        {
            var cart = await _unitOfWork.ShoppingCarts.GetByUserIdAsync(userId);
            if (cart == null) return true;

            cart.Items.Clear();
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<ShoppingCartDto> GetCartByUserIdAsync(Guid userId)
        {
            var cart = await GetOrCreateCartByUserIdAsync(userId);
            return MapCartToDto(cart);
        }

        public async Task<bool> RemoveItemFromCartAsync(Guid userId, Guid gameId)
        {
            var cart = await _unitOfWork.ShoppingCarts.GetByUserIdAsync(userId);
            if (cart == null) return false;

            var cartItem = cart.Items.FirstOrDefault(i => i.GameId == gameId);
            if (cartItem == null) return false;

            cart.Items.Remove(cartItem);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        private async Task<ShoppingCart> GetOrCreateCartByUserIdAsync(Guid userId)
        {
            var cart = await _unitOfWork.ShoppingCarts.GetByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new ShoppingCart { Id = Guid.NewGuid(), UserId = userId };
                await _unitOfWork.ShoppingCarts.AddAsync(cart);
                await _unitOfWork.CompleteAsync();
            }
            return cart;
        }

        private ShoppingCartDto MapCartToDto(ShoppingCart cart)
        {
            var cartDto = new ShoppingCartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = [.. cart.Items.Select(item => new CartItemDto
                {
                    GameId = item.GameId,
                    GameName = item.Game?.Name ?? "N/A",
                    Price = item.Game?.Price ?? 0,
                    Quantity = item.Quantity
                })]
            };
            cartDto.TotalPrice = cartDto.Items.Sum(i => i.Price * i.Quantity);
            return cartDto;
        }
    }
}