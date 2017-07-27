using System;

using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public class UpdateOneResult<TModel>: OperationResult<TModel>, IUpdateOneResult<TModel>
        where TModel : class, IEntity
    {
        public UpdateOneResult() : base(null)
        {}
        
        public UpdateOneResult(Exception exception) : base(exception)
        {}
    }
}
