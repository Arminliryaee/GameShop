using System.ComponentModel.DataAnnotations;

namespace GameShop.Application.Dtos.UserDtos
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
