using MongoDB.Driver;
using Pilcrow.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pilcrow.Db.Repositories.Results
{
    public class FindManyResult<TModel>: OperationResult<TModel>, IFindManyResult<TModel>
        where TModel : class, IEntity
    {
        protected IFindFluent<TModel, TModel> FluentObjects;
        
        public IEnumerable<TModel> Objects => FluentObjects?.ToCursor().ToEnumerable() ?? new List<TModel>();
        
        public FindManyResult() : base(null)
        {}
        
        public FindManyResult(Exception exception) : base(exception)
        {
            FluentObjects = null;
        }
        
        public FindManyResult(IFindFluent<TModel, TModel> fluentObjects) : base(null)
        {
            FluentObjects = fluentObjects;
        }
        
        public long Count()
        {
            return FluentObjects?.Count() ?? 0;
        }
        
        public IFindManyResult<TModel> Skip(int n)
        {
            FluentObjects = FluentObjects?.Skip(n);
            return this;
        }
        
        public IFindManyResult<TModel> Take(int n)
        {
            FluentObjects = FluentObjects?.Limit(n);
            return this;
        }
        
        public IFindManyResult<TModel> SortByAscending(Expression<Func<TModel, object>> field)
        {
            FluentObjects = FluentObjects?.SortBy(field);
            return this;
        }
        
        public IFindManyResult<TModel> SortByDescending(Expression<Func<TModel, object>> field)
        {
            FluentObjects = FluentObjects?.SortByDescending(field);
            return this;
        }
    }
}
