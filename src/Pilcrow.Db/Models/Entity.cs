
using System;
using System.Linq;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Pilcrow.Core.Helpers;

namespace Pilcrow.Db.Models
{
    public abstract class Entity : IEntity
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator)), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public ObjectId ObjectId => ObjectId.Parse(Id);
        
        public DateTime CreationTime => ObjectId.CreationTime;
        
        public DateTime ModificationTime { get; set; }
        
        public static void SetupClassMaps()
        {
            foreach (var modelType in TypeHelper.GetDirectSubClassTypes(typeof(Entity))) {
                var modelBsonClassMap = BsonClassMap.LookupClassMap(modelType);
                modelBsonClassMap.AutoMap();
                foreach (var modelSubType in TypeHelper.GetSubClassTypes(modelType)) {
                    BsonClassMap.LookupClassMap(modelSubType).AutoMap();
                    modelBsonClassMap.AddKnownType(modelSubType);
                }
            }
        }
    }
}
