using GameShop.Application.Dtos.GameDtos;
using GameShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameShop.Presentation.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly IGameService _gameService;
        public DeleteModel(IGameService gameService) => _gameService = gameService;

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            await _gameService.DeleteGameAsync(id.Value);
            return RedirectToPage("./Index");
        }
    }
}