using Pilcrow.Db.Models;
using System;

namespace Pilcrow.Db.Repositories.Results
{
    public interface IOperationResult<TModel>
        where TModel : class, IEntity
    {
        Exception Exception { get; set; }
        
        Type ExceptionType { get; }
        
        Type ModelType { get; }
        
        bool Success { get; }
    }
}
