using BallastLaneApplication.Core.Dtos;
using BallastLaneApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallastLaneApplication.Core.Interfaces.Service
{
    public interface IUserService
    {
        Task<UserEntitie> AuthenticateAsync(string username, string password);
        Task<UserEntitie> GetUserByIdAsync(Guid id);
        Task<UserEntitie> GetByUserNameAsync(string username);
        Task<UserEntitie> CreateUserAsync(UserRegisterDto user, bool admin = false);
        Task UpdateUserAsync(UserEntitie user);
        Task DeleteUserAsync(Guid id);
    }
}
