
using System;
using Pilcrow.Db.Models;

namespace Pilcrow.Db.Helpers
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
