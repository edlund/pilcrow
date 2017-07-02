using System;

using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public class FindOneResult<TModel>: OperationResult<TModel>, IFindOneResult<TModel>
        where TModel : class, IEntity
    {
        public TModel Object { get; private set; }
        
        public FindOneResult() : base(null)
        {}
        
        public FindOneResult(Exception exception) : base(exception)
        {
            Object = null;
        }
        
        public FindOneResult(TModel obj) : base(null)
        {
            Object = obj;
        }
    }
}
