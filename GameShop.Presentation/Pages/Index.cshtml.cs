using GameShop.Application.Dtos.GameDtos;
using GameShop.Application.Dtos.ShoppingCartDtos;
using GameShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace GameShop.Presentation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGameService _gameService;
        private readonly IShoppingCartService _cartService;
        public IndexModel(IGameService gameService, IShoppingCartService cartService)
        {
            _gameService = gameService;
            _cartService = cartService;
        }

        public IEnumerable<GameDto> Games { get; set; }
        public async Task OnGetAsync() => Games = await _gameService.GetAllGamesAsync();
        public async Task<IActionResult> OnPostAddToCartAsync(Guid gameId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Account/Login");
            
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var addItemDto = new AddItemToCartDto
            {
                GameId = gameId,
                Quantity = 1
            };

            await _cartService.AddItemToCartAsync(userId, addItemDto);
            return RedirectToPage("/Cart");
        }
    }
}