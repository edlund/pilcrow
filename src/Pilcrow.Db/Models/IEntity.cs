using MongoDB.Bson;
using System;

namespace Pilcrow.Db.Models
{
    public interface IEntity
    {
        string Id { get; set; }
        
        ObjectId ObjectId { get; }
        
        DateTime CreationTime { get; }
        
        DateTime ModificationTime { get; set; }
    }
}
