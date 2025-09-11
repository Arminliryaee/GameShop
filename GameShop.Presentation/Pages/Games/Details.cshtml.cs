using GameShop.Application.Dtos.GameDtos;
using GameShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameShop.Presentation.Pages.Games
{
    public class DetailsModel : PageModel
    {
        private readonly IGameService _gameService;
        public DetailsModel(IGameService gameService) => _gameService = gameService;

        public GameDto Game { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            Game = await _gameService.GetGameByIdAsync(id.Value);

            if (Game == null)
                return NotFound();

            return Page();
        }
    }
}