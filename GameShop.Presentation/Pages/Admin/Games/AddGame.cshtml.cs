using GameShop.Application.Dtos.GameDtos;
using GameShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameShop.Presentation.Pages.Admin.Games
{
    [Authorize(Roles = "Admin")]
    public class AddGameModel : PageModel
    {
        private readonly IGameService _gameService;
        public AddGameModel(IGameService gameService) => _gameService = gameService;

        [BindProperty]
        public AddGameDto Game { get; set; }
        public IActionResult OnGet() => Page();
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _gameService.CreateGameAsync(Game);
            return RedirectToPage("./Index");
        }
    }
}