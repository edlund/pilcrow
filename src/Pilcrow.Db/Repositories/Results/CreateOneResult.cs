using System;

using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public class CreateOneResult<TModel>: OperationResult<TModel>, ICreateOneResult<TModel>
        where TModel : class, IEntity
    {
        public CreateOneResult() : base(null)
        {}
        
        public CreateOneResult(Exception exception) : base(exception)
        {}
    }
}
