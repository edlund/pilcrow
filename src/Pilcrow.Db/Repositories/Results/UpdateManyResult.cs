using System;

using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public class UpdateManyResult<TModel>: OperationResult<TModel>, IUpdateManyResult<TModel>
        where TModel : class, IEntity
    {
        public UpdateManyResult() : base(null)
        {}
        
        public UpdateManyResult(Exception exception) : base(exception)
        {}
    }
}
