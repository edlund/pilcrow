using Pilcrow.Db.Models;
using System;

namespace Pilcrow.Db.Repositories.Results
{
    public abstract class OperationResult<TModel> : IOperationResult<TModel>
        where TModel : class, IEntity
    {
        public Exception Error { get; set; }
        
        public Type ExceptionType => Error.GetType();
        
        public Type ModelType => typeof(TModel);
        
        public bool Success => Error == null;
        
        public OperationResult(Exception error)
        {
            Error = error;
        }
    }
}
