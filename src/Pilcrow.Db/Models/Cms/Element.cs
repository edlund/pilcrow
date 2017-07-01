using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Pilcrow.Db.Models.Cms
{
    public abstract class Element : Entity, IAutoMappable
    {
        public List<Element> Children { get; set; } = new List<Element>();
    }
}
