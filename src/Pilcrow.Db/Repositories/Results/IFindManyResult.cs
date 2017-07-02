using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public interface IFindManyResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
        IEnumerable<TModel> Objects { get; }
        
        long Count();
        
        IFindManyResult<TModel> Skip(int n);
        
        IFindManyResult<TModel> Take(int n);
        
        IFindManyResult<TModel> SortByAscending(Expression<Func<TModel, object>> field);
        
        IFindManyResult<TModel> SortByDescending(Expression<Func<TModel, object>> field);
    }
}
