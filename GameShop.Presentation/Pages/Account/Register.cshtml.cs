using GameShop.Application.Dtos.UserDtos;
using GameShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameShop.Presentation.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;
        public RegisterModel(IUserService userService) => _userService = userService;

        [BindProperty]
        public CreateUserDto RegisterDto { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                await _userService.RegisterUserAsync(RegisterDto);
                return RedirectToPage("/Account/Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
