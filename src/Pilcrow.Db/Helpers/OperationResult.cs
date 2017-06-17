
using System;
using Pilcrow.Db.Models;

namespace Pilcrow.Db.Helpers
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
