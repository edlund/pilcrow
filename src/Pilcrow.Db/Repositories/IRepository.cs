using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Pilcrow.Db.Models;
using Pilcrow.Db.Repositories.Results;

namespace Pilcrow.Db.Repositories
{
    public interface IRepository
    {
    }
    
    public interface IRepository<TModel> : IRepository
        where TModel : class, IEntity
    {
        string CollectionName(Type type);
        
        bool ValidateObjectId(string value);
        
        void Create(TModel entity);
        
        void Create(TModel entity, ICreateResult<TModel> result);
        
        void Update(TModel entity);
        
        void Update(TModel entity, IUpdateResult<TModel> result);
        
        void Delete(TModel entity);
        
        void Delete(TModel entity, IDeleteResult<TModel> result);
        
        IFindOneResult<TModel> FindOne(string id, bool throwOnError = true);
        
        IFindOneResult<TModel> FindOne(
            Expression<Func<TModel, bool>> filter,
            bool throwOnError = true
        );
        
        IFindManyResult<TModel> FindMany(
            Expression<Func<TModel, bool>> filter,
            bool throwOnError = true
        );
    }
}
