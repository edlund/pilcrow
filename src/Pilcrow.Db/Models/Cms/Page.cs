using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Pilcrow.Db.Models.Cms
{
    public class Page : Entity, IAutoMappable, IPublishable, IRevisionable
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ParentId { get; set; }
        
        public string Uuid { get; set; }
        
        public bool Live { get; set; }
        
        public int Version { get; set; }
        
        public List<Element> Elements { get; set; } = new List<Element>();
    }
}
