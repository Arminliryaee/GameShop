using GameShop.Application.Dtos.UserDtos;

namespace GameShop.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterUserAsync(CreateUserDto createUserDto);
        Task<AuthResponseDto> LoginUserAsync(LoginDto loginDto);
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(UpdateUserDto updateUserDto);
    }
}