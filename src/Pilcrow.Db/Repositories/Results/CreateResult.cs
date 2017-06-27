using Pilcrow.Db.Models;
using System;

namespace Pilcrow.Db.Repositories.Results
{
    public class CreateResult<TModel>: OperationResult<TModel>, ICreateResult<TModel>
        where TModel : class, IEntity
    {
        public CreateResult() : base(null)
        {}
        
        public CreateResult(Exception error) : base(error)
        {}
    }
}
