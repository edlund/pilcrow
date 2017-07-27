using System;

using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public class DeleteManyResult<TModel>: OperationResult<TModel>, IDeleteManyResult<TModel>
        where TModel : class, IEntity
    {
        public DeleteManyResult() : base(null)
        {}
        
        public DeleteManyResult(Exception exception) : base(exception)
        {}
    }
}
