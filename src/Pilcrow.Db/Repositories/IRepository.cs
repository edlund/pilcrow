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
        
        void CreateOne(TModel entity);
        
        void CreateOne(TModel entity, ICreateOneResult<TModel> result);
        
        void CreateMany(IEnumerable<TModel> entities);
        
        void CreateMany(IEnumerable<TModel> entities, ICreateManyResult<TModel> result);
        
        void UpdateOne(TModel entity);
        
        void UpdateOne(TModel entity, IUpdateOneResult<TModel> result);
        
        void UpdateMany(IEnumerable<TModel> entities);
        
        void UpdateMany(IEnumerable<TModel> entities, ICreateManyResult<TModel> result);
        
        void DeleteOne(TModel entity);
        
        void DeleteOne(TModel entity, IDeleteOneResult<TModel> result);
        
        void DeleteMany(IEnumerable<TModel> entities);
        
        void DeleteMany(IEnumerable<TModel> entities, ICreateManyResult<TModel> result);
        
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
