using GameShop.Application.Dtos.ShoppingCartDtos;
using GameShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace GameShop.Presentation.Pages
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly IShoppingCartService _cartService;
        private readonly IOrderService _orderService;
        public CartModel(IShoppingCartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public ShoppingCartDto Cart { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Cart = await _cartService.GetCartByUserIdAsync(userId);
            return Page();
        }
        public async Task<IActionResult> OnPostRemoveItemAsync(Guid gameId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _cartService.RemoveItemFromCartAsync(userId, gameId);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = await _orderService.CreateOrderFromCartAsync(userId);
            return RedirectToPage("/OrderSuccess", new { orderId = order.Id });
        }
    }
}