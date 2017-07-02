using System;

using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public abstract class OperationResult<TModel> : IOperationResult<TModel>
        where TModel : class, IEntity
    {
        public Exception Exception { get; set; }
        
        public Type ExceptionType => Exception.GetType();
        
        public Type ModelType => typeof(TModel);
        
        public bool Success => Exception == null;
        
        public OperationResult(Exception exception)
        {
            Exception = exception;
        }
    }
}
