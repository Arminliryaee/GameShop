using GameShop.Application.Dtos.UserDtos;
using GameShop.Application.Services.Interfaces;
using GameShop.Domain.Entities;
using GameShop.Domain.Interfaces;


namespace GameShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _token;

        public UserService(IUnitOfWork unitOfWork, ITokenService token)
        {
            _unitOfWork = unitOfWork;
            _token = token;
        }


        public async Task<UserDto> RegisterUserAsync(CreateUserDto createUserDto)
        {
            if (await _unitOfWork.Users.GetByUsernameAsync(createUserDto.Username) != null)
                throw new Exception("Username already exists.");


            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Age = createUserDto.Age,
                Address = createUserDto.Address,
                Phone = createUserDto.Phone,
                HashPassword = hashedPassword,
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return MapUserToDto(user);
        }

        public async Task<AuthResponseDto> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(loginDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.HashPassword))
                throw new Exception("Invalid username or password.");


            var token = _token.CreateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                User = MapUserToDto(user)
            };
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) return false;

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return users.Select(MapUserToDto);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            return user != null ? MapUserToDto(user) : null;
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(updateUserDto.Id) ?? throw new Exception("User not found.");

            user.FirstName = !string.IsNullOrWhiteSpace(updateUserDto.FirstName) ? updateUserDto.FirstName : user.FirstName;
            user.LastName = !string.IsNullOrWhiteSpace(updateUserDto.LastName) ? updateUserDto.LastName : user.LastName;
            user.Address = !string.IsNullOrWhiteSpace(updateUserDto.Address) ? updateUserDto.Address : user.Address;
            user.Phone = !string.IsNullOrWhiteSpace(updateUserDto.Phone) ? updateUserDto.Phone : user.Phone;
            user.Email = !string.IsNullOrWhiteSpace(updateUserDto.Email) ? updateUserDto.Email : user.Email;

            if (updateUserDto.Age > 0)
                user.Age = updateUserDto.Age;

            if (!string.IsNullOrWhiteSpace(updateUserDto.NewPassword))
            {
                if (string.IsNullOrWhiteSpace(updateUserDto.OldPassword))
                    throw new Exception("Old password is required to set a new one.");

                if (!BCrypt.Net.BCrypt.Verify(updateUserDto.OldPassword, user.HashPassword))
                    throw new Exception("Incorrect old password.");

                if (updateUserDto.NewPassword != updateUserDto.ConfirmNewPassword)
                    throw new Exception("New password and confirmation do not match.");

                user.HashPassword = BCrypt.Net.BCrypt.HashPassword(updateUserDto.NewPassword);
            }

            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        private UserDto MapUserToDto(User user) => new()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Age = user.Age,
            Address = user.Address,
            Phone = user.Phone,
            UserRole = user.UserRole.ToString()
        };
    }
}