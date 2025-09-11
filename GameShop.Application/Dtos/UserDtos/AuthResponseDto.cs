namespace GameShop.Application.Dtos.UserDtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}