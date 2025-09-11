using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameShop.Presentation.Pages
{
    public class OrderSuccessModel : PageModel
    {
        public Guid OrderId { get; set; }
        public void OnGet(Guid orderId) => OrderId = orderId;
        
    }
}
