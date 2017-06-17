
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pilcrow.Db.Helpers;
using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories
{
    public interface IRepository<TModel>
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
