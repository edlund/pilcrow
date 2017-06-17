
using System;
using Pilcrow.Db.Models;

namespace Pilcrow.Db.Helpers
{
    public interface IOperationResult<TModel>
        where TModel : class, IEntity
    {
        Exception Error { get; set; }
        
        Type ExceptionType { get; }
        
        Type ModelType { get; }
        
        bool Success { get; }
    }
}
