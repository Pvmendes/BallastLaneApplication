using BallastLaneApplication.Core.Interfaces.Repository;
using MongoDB.Driver;
using BallastLaneApplication.Core.Entities;
using BallastLaneApplication.Infrastructure.Data.MongoDB;

namespace BallastLaneApplication.Infrastructure.Data.MongoDB.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<TaskEntitie> _tasksCollection;

        public TaskRepository(MongoDbContext context)
        {
            _tasksCollection = context.Database.GetCollection<TaskEntitie>("Tasks");
        }

        public async Task<IEnumerable<TaskEntitie>> GetAllAsync()
        {
            return await _tasksCollection.Find(task => true).ToListAsync();
        }

        public async Task<IEnumerable<TaskEntitie>> GetAllByUserNameAsync(string userName)
        {
            return await _tasksCollection.Find(task => task.UserName == userName).ToListAsync();
        }

        public async Task<TaskEntitie> GetByIdAsync(Guid id)
        {
            return await _tasksCollection.Find(task => task.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TaskEntitie> AddAsync(TaskEntitie task)
        {
            await _tasksCollection.InsertOneAsync(task);
            return task;
        }

        public async Task UpdateAsync(TaskEntitie task)
        {
            await _tasksCollection.ReplaceOneAsync(t => t.Id == task.Id, task);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _tasksCollection.DeleteOneAsync(task => task.Id == id);
        }
    }
}
