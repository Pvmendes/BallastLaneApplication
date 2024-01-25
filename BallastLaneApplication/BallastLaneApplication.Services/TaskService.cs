using BallastLaneApplication.Core.Dtos;
using BallastLaneApplication.Core.Entities;
using BallastLaneApplication.Core.Interfaces.Repository;
using BallastLaneApplication.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallastLaneApplication.Services
{
    public class TaskService: ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskEntitie>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TaskEntitie>> GetAllByUserNameAsync(string userName)
        {
            return await _taskRepository.GetAllByUserNameAsync(userName);
        }

        public async Task<TaskEntitie> GetTaskByIdAsync(Guid id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<TaskEntitie> CreateTaskAsync(TaskEntitie task)
        {
            return await _taskRepository.AddAsync(task);
        }

        public async Task UpdateTaskAsync(TaskEntitie task)
        {
            task.UpdatedAt = DateTime.Now;
            await _taskRepository.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        }
    }
}
