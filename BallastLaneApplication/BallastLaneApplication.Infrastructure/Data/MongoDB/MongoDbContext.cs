using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallastLaneApplication.Infrastructure.Data.MongoDB
{
    public class MongoDbContext
    {
        public IMongoDatabase Database { get; }

        public MongoDbContext(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(dbName);
        }
    }
}
