
using System;
using Pilcrow.Db.Models;

namespace Pilcrow.Db.Helpers
{
    public class DeleteResult<TModel>: OperationResult<TModel>, IDeleteResult<TModel>
        where TModel : class, IEntity
    {
        public DeleteResult() : base(null)
        {}
        
        public DeleteResult(Exception error) : base(error)
        {}
    }
}
