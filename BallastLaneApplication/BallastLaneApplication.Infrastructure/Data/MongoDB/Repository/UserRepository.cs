using BallastLaneApplication.Core.Entities;
using BallastLaneApplication.Infrastructure.Data.MongoDB;
using BallastLaneApplication.Infrastructure.Interfaces.Repository;
using MongoDB.Driver;

namespace BallastLaneApplication.Infrastructure.Data.MongoDB.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntitie> _usersCollection;

        public UserRepository(MongoDbContext context)
        {
            _usersCollection = context.Database.GetCollection<UserEntitie>("Users");
            CreateUniqueIndexOnUsername();
        }

        private void CreateUniqueIndexOnUsername()
        {
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexKeys = Builders<UserEntitie>.IndexKeys.Ascending(user => user.Username);
            var indexModel = new CreateIndexModel<UserEntitie>(indexKeys, indexOptions);
            _usersCollection.Indexes.CreateOne(indexModel);
        }

        public async Task<UserEntitie> GetByIdAsync(Guid id)
        {
            return await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserEntitie> GetByUserNameAsync(string username)
        {
            return await _usersCollection.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task<UserEntitie> AddAsync(UserEntitie user)
        {
            try
            {
                await _usersCollection.InsertOneAsync(user);
                return user;
            }
            catch (MongoWriteException ex)
            {
                if (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
                {
                    // Handle the duplicate key error
                    throw new Exception("A user with this username already exists.");
                }
                throw; // Re-throw the exception if it's not a duplicate key error
            }
        }

        public async Task UpdateAsync(UserEntitie user)
        {
            await _usersCollection.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _usersCollection.DeleteOneAsync(user => user.Id == id);
        }
    }
}
