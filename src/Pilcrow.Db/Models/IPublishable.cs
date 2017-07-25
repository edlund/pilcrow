using System;

namespace Pilcrow.Db.Models
{
    public interface IPublishable
    {
        Guid Guid { get; set; }
        
        bool Live { get; set; }
    }
}
