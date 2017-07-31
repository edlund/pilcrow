using System;

using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Pilcrow.Db.Models;
using Pilcrow.Db.Repositories;

namespace Pilcrow.Db.Migrations
{
    public abstract class Migration : IMigratable
    {
        public IContext Context { get; set; }
        
        public IConfigurationRoot Configuration { get; set; }
        
        protected IMongoCollection<BsonDocument> GetCollection(string name)
        {
            return Context.Database.GetCollection<BsonDocument>(name);
        }
        
        protected IMongoCollection<BsonDocument> GetCollection<TModel>()
            where TModel : class, IEntity
        {
            return GetCollection(Repository.CollectionName(typeof(TModel)));
        }
        
        protected void WalkCollection(
            IMongoCollection<BsonDocument> collection,
            Action<BsonDocument> walker)
        {
            var cursor = collection.Find(x => true);
            var count = cursor.Count();
            var skip = 0;
            var limit = int.Parse(Configuration["Database:Migrations:BatchSize"]);
            
            do
            {
                foreach (var entity in cursor.Skip(skip).Limit(limit).ToList())
                {
                    walker(entity);
                    var id = entity["_id"];
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
                    collection.ReplaceOne(filter, entity);
                }
                skip += limit;
            }
            while (skip < count);
        }
        
        public abstract void Execute();
    }
}
