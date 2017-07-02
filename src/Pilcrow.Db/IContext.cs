using MongoDB.Driver;

namespace Pilcrow.Db
{
    public interface IContext
    {
        IMongoClient Client { get; }
        
        IMongoDatabase Database { get; }
        
        void Connect(string connectionString, string databaseName);
        
        void DropDatabase(string databaseName);
    }
}
