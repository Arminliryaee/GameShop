using System.ComponentModel.DataAnnotations;

namespace GameShop.Application.Dtos.ShoppingCartDtos
{
    public class AddItemToCartDto
    {
        [Required]
        public Guid GameId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}