using System;
using System.Linq;

using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Pilcrow.Core.Helpers;

namespace Pilcrow.Db.Migrations
{
    public class MigrationRunner
    {
        public string CollectionName => "_migration";
        
        public string CollectionFieldName => "Migration";
        
        private readonly IContext _context;
        
        private readonly IConfigurationRoot _configuration;
        
        public MigrationRunner(IContext context, IConfigurationRoot configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        public void Execute()
        {
            var collection = _context.Database.GetCollection<BsonDocument>(CollectionName);
            
            var allTypes = TypeHelper.GetImplementingTypes(typeof(IMigratable));
            var concreteTypes = TypeHelper.GetConcreteTypes(allTypes);
            foreach (var concreteType in concreteTypes)
            {
                var filter = Builders<BsonDocument>.Filter.Eq(CollectionFieldName, concreteType.FullName);
                if (collection.Count(filter) > 0)
                    continue;
                
                var migration = (IMigratable)Activator.CreateInstance(concreteType);
                migration.Context = _context;
                migration.Configuration = _configuration;
                migration.Execute();
                
                collection.InsertOne(new BsonDocument
                {
                    { CollectionFieldName, concreteType.FullName }
                });
            }
        }
    }
}
