using System;

using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public class DeleteOneResult<TModel>: OperationResult<TModel>, IDeleteOneResult<TModel>
        where TModel : class, IEntity
    {
        public DeleteOneResult() : base(null)
        {}
        
        public DeleteOneResult(Exception exception) : base(exception)
        {}
    }
}
