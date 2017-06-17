
using MongoDB.Driver;

namespace Pilcrow.Db
{
    public class Context : IContext
    {
        public IMongoClient Client { get; private set; }
        
        public IMongoDatabase Database { get; private set; }
        
        public void Connect(string connectionString, string databaseName)
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
        }
    }
}
