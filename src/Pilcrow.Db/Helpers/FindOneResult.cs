
using System;
using Pilcrow.Db.Models;

namespace Pilcrow.Db.Helpers
{
    public class FindOneResult<TModel>: OperationResult<TModel>, IFindOneResult<TModel>
        where TModel : class, IEntity
    {
        public TModel Object { get; private set; }
        
        public FindOneResult() : base(null)
        {}
        
        public FindOneResult(Exception error) : base(error)
        {
            Object = null;
        }
        
        public FindOneResult(TModel obj) : base(null)
        {
            Object = obj;
        }
    }
}
