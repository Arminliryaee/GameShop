using GameShop.Application.Dtos.GameDtos;
using GameShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameShop.Presentation.Pages.Admin.Games
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IGameService _gameService;
        public DeleteModel(IGameService gameService) => _gameService = gameService;
        
        [BindProperty]
        public GameDto Game { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Game = await _gameService.GetGameByIdAsync(id);

            if (Game == null)
                return NotFound();
          
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            await _gameService.DeleteGameAsync(id);
            return RedirectToPage("./Index");
        }
    }
}