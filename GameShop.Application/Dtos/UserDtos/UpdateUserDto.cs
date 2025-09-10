using System.ComponentModel.DataAnnotations;

namespace GameShop.Application.Dtos.UserDtos
{
    public class UpdateUserDto
    {
        [Required]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "New password must be at least 8 characters long.")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
        [Range(0, 100)]
        public int Age { get; set; }
        [StringLength(200, MinimumLength = 10)]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
