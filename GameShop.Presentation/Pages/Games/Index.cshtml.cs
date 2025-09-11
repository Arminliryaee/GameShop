using GameShop.Application.Dtos.GameDtos;
using GameShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameShop.Presentation.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly IGameService _gameService;
        public IndexModel(IGameService gameService) => _gameService = gameService;

        public IEnumerable<GameDto> Games { get; set; }
        public async Task OnGetAsync() => Games = await _gameService.GetAllGamesAsync();
    }
}