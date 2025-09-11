using GameShop.Application.Dtos.UserDtos;
using GameShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace GameShop.Presentation.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        public LoginModel(IUserService userService) => _userService = userService;

        [BindProperty]
        public LoginDto LoginDto { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl = "/") => ReturnUrl = returnUrl;
        public async Task<IActionResult> OnPostAsync(string returnUrl = "/")
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var authResponse = await _userService.LoginUserAsync(LoginDto);

                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, authResponse.User.Id.ToString()),
                    new(ClaimTypes.Name, authResponse.User.Username),
                    new(ClaimTypes.Role, authResponse.User.UserRole)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
