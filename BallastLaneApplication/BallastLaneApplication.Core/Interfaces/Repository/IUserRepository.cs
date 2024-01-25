
using BallastLaneApplication.Core.Entities;

namespace BallastLaneApplication.Infrastructure.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<UserEntitie> GetByIdAsync(Guid id);
        Task<UserEntitie> GetByUserNameAsync(string username);
        Task<UserEntitie> AddAsync(UserEntitie user);
        Task UpdateAsync(UserEntitie user);
        Task DeleteAsync(Guid id);
    }
}
