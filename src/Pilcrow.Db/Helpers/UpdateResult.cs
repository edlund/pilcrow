
using System;
using Pilcrow.Db.Models;

namespace Pilcrow.Db.Helpers
{
    public class UpdateResult<TModel>: OperationResult<TModel>, IUpdateResult<TModel>
        where TModel : class, IEntity
    {
        public UpdateResult() : base(null)
        {}
        
        public UpdateResult(Exception error) : base(error)
        {}
    }
}