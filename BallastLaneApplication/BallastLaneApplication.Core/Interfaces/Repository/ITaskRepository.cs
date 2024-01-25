
using BallastLaneApplication.Core.Entities;

namespace BallastLaneApplication.Core.Interfaces.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntitie>> GetAllAsync();
        Task<IEnumerable<TaskEntitie>> GetAllByUserNameAsync(string userName);
        Task<TaskEntitie> GetByIdAsync(Guid id);
        Task<TaskEntitie> AddAsync(TaskEntitie task);
        Task UpdateAsync(TaskEntitie task);
        Task DeleteAsync(Guid id);
    }
}
