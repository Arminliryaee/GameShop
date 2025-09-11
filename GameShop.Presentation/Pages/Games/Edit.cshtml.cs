using GameShop.Application.Dtos.GameDtos;
using GameShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameShop.Presentation.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly IGameService _gameService;
        public EditModel(IGameService gameService)=>_gameService = gameService;
        
        [BindProperty]
        public UpdateGameDto Game { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            var gameDto = await _gameService.GetGameByIdAsync(id.Value);
            if (gameDto == null)
                return NotFound();

            Game = new UpdateGameDto
            {
                Id = gameDto.Id,
                Name = gameDto.Name,
                Description = gameDto.Description,
                Price = gameDto.Price,
                StockLevel = gameDto.StockLevel,
                Platform = gameDto.Platform
            };

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _gameService.UpdateGameAsync(Game);
            return RedirectToPage("./Index");
        }
    }
}