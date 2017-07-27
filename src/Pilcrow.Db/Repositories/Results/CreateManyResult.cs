using System;

using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public class CreateManyResult<TModel>: OperationResult<TModel>, ICreateManyResult<TModel>
        where TModel : class, IEntity
    {
        public CreateManyResult() : base(null)
        {}
        
        public CreateManyResult(Exception exception) : base(exception)
        {}
    }
}
