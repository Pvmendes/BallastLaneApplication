using BallastLaneApplication.Core.Dtos;
using BallastLaneApplication.Core.Entities;

namespace BallastLaneApplication.Core.Interfaces.Service
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskEntitie>> GetAllTasksAsync();
        Task<IEnumerable<TaskEntitie>> GetAllByUserNameAsync(string userName);
        Task<TaskEntitie> GetTaskByIdAsync(Guid id);
        Task<TaskEntitie> CreateTaskAsync(TaskEntitie task);
        Task UpdateTaskAsync(TaskEntitie task);
        Task DeleteTaskAsync(Guid id);
    }
}
