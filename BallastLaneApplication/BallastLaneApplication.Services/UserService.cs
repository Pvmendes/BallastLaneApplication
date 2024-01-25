using BallastLaneApplication.Core.Dtos;
using BallastLaneApplication.Core.Entities;
using BallastLaneApplication.Core.Interfaces.Service;
using BallastLaneApplication.Infrastructure.Interfaces.Repository;

namespace BallastLaneApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntitie> AuthenticateAsync(string username, string password)
        {
            // TODO: Implement your authentication logic here
            var user = await _userRepository.GetByUserNameAsync(username);

            // You should encrypt and compare passwords, this is just a basic example
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }

        public async Task<UserEntitie> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<UserEntitie> GetByUserNameAsync(string username)
        {
            return await _userRepository.GetByUserNameAsync(username);
        }

        public async Task<UserEntitie> CreateUserAsync(UserRegisterDto user, bool admin = false)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            return await _userRepository.AddAsync(
                new UserEntitie() {
                    Username = user.Username, 
                    Role = admin? "admin" : "user", 
                    PasswordHash = passwordHash 
                });
        }

        public async Task UpdateUserAsync(UserEntitie user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
