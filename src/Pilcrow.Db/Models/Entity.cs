using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Pilcrow.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pilcrow.Db.Models
{
    /// <summary>
    /// Base class for Database Models.
    /// </summary>
    public abstract class Entity : IEntity
    {
        public static bool ClassMapsRegistered { get; private set; } = false;
        
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator)), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public ObjectId ObjectId => ObjectId.Parse(Id);
        
        public DateTime CreationTime => ObjectId.CreationTime;
        
        public DateTime ModificationTime { get; set; }
        
        /// <summary>
        /// Handle "automagical" BsonClassMap registration for Models
        /// that does not implement IUnautoMappable.
        /// </summary>
        public static void RegisterClassMaps()
        {
            if (ClassMapsRegistered)
                return;
            
            Func<Type, BsonClassMap> createClassMap = classType =>
            {
                var classMapDefinitionType = typeof(BsonClassMap<>);
                var classMapType = classMapDefinitionType.MakeGenericType(classType);
                var classMap = (BsonClassMap)Activator.CreateInstance(classMapType);
                classMap.AutoMap();
                BsonClassMap.RegisterClassMap(classMap);
                return classMap;
            };
            Func<Type, bool> autoMappable = classType => classType
                .GetInterfaces()
                .Contains(typeof(IAutoMappable));
            
            var classMaps = new List<BsonClassMap>();
            var modelTypes = TypeHelper.GetDirectSubClassTypes(typeof(Entity)).Where(autoMappable);
            
            foreach (var modelType in modelTypes)
            {
                var classMap = createClassMap(modelType);
                var modelSubTypes = TypeHelper.GetSubClassTypes(modelType).Where(autoMappable);
                foreach (var modelSubType in modelSubTypes)
                {
                    var subClassMap = createClassMap(modelSubType);
                    classMap.AddKnownType(modelSubType);
                    classMaps.Add(subClassMap);
                }
                classMaps.Add(classMap);
            }
            classMaps.ForEach(classMap => classMap.Freeze());
            
            ClassMapsRegistered = true;
        }
    }
}
