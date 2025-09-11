using GameShop.Application.Dtos.OrderDtos;
using GameShop.Application.Dtos.UserDtos;
using GameShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace GameShop.Presentation.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public ProfileModel(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        [BindProperty]
        public UpdateUserDto UpdateUser { get; set; }
        public UserDto CurrentUser { get; set; }
        public IEnumerable<OrderDto> OrderHistory { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            CurrentUser = await _userService.GetUserByIdAsync(userId);
            if (CurrentUser == null)
                return NotFound();

            UpdateUser = new UpdateUserDto { Id = CurrentUser.Id, Email = CurrentUser.Email, FirstName = CurrentUser.FirstName, LastName = CurrentUser.LastName, Phone = CurrentUser.Phone, Address = CurrentUser.Address, Age = CurrentUser.Age };
            OrderHistory = await _orderService.GetOrdersByUserIdAsync(userId);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                await _userService.UpdateUserAsync(UpdateUser);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}