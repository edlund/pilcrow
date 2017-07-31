using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using MongoDB.Bson;
using MongoDB.Driver;
using Pilcrow.Db.Models;
using Pilcrow.Db.Repositories.Exceptions;
using Pilcrow.Db.Repositories.Results;

namespace Pilcrow.Db.Repositories
{
    public abstract class Repository
    {
        protected readonly IContext Context;
        
        protected Repository(IContext context)
        {
            Context = context;
        }
        
        public static string CollectionName(Type type)
        {
            return $"{type.FullName}".Replace('.', '_').ToLower();
        }
        
        public static bool ValidateObjectId(string value)
        {
            ObjectId objectId;
            return ObjectId.TryParse(value, out objectId);
        }
    }
    
    public abstract class Repository<TModel> : Repository, IRepository<TModel>
        where TModel : class, IEntity
    {
        protected readonly IMongoCollection<TModel> Collection;
        
        protected Repository(IContext context) : base(context)
        {
            Collection = Context.Database.GetCollection<TModel>(CollectionName(typeof(TModel)));
        }
        
        private void ExecuteOneOperation(
            Action<TModel> operation,
            TModel entity,
            IOperationResult<TModel> result)
        {
            try
            {
                operation(entity);
            }
            catch (Exception error)
            {
                result.Exception = error;
            }
        }
        
        private void ExecuteManyOperation(
            Action<IEnumerable<TModel>> operation,
            IEnumerable<TModel> entities,
            IOperationResult<TModel> result)
        {
            try
            {
                operation(entities);
            }
            catch (Exception error)
            {
                result.Exception = error;
            }
        }
        
        private void RequireNewObject(TModel entity)
        {
            if (!string.IsNullOrEmpty(entity.Id))
                throw new IdIsNotNullOrEmptyException(entity.Id);
            if (entity.ModificationTime == DateTime.MinValue)
                entity.ModificationTime = DateTime.Now;
        }
        
        private void RequireExistingObject(TModel entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(entity.Id))
                throw new IdIsNullOrEmptyException(entity.GetType());
            if (!ValidateObjectId(entity.Id))
                throw new InvalidIdException(entity.Id);
        }
        
        public new string CollectionName(Type type)
        {
            return Repository.CollectionName(type);
        }
        
        public new bool ValidateObjectId(string value)
        {
            return Repository.ValidateObjectId(value);
        }
        
        public void CreateOne(TModel entity)
        {
            RequireNewObject(entity);
            Collection.InsertOne(entity);
        }
        
        public void CreateOne(TModel entity, ICreateOneResult<TModel> result)
        {
            ExecuteOneOperation(CreateOne, entity, result);
        }
        
        public void CreateMany(IEnumerable<TModel> entities)
        {
            entities.ToList().ForEach(RequireNewObject);
            Collection.InsertMany(entities);
        }
        
        public void CreateMany(IEnumerable<TModel> entities, ICreateManyResult<TModel> result)
        {
            ExecuteManyOperation(CreateMany, entities, result);
        }
        
        public void UpdateOne(TModel entity)
        {
            RequireExistingObject(entity);
            entity.ModificationTime = DateTime.Now;
            var filter = Builders<TModel>.Filter.Eq("_id", entity.ObjectId);
            var result = Collection.ReplaceOne(filter, entity);
            if (result.ModifiedCount != 1)
                throw new UnexpectedResultException(entity.Id, "update");
        }
        
        public void UpdateOne(TModel entity, IUpdateOneResult<TModel> result)
        {
            ExecuteOneOperation(UpdateOne, entity, result);
        }
        
        public void UpdateMany(IEnumerable<TModel> entities)
        {
            entities.ToList().ForEach(UpdateOne);
        }
        
        public void UpdateMany(IEnumerable<TModel> entities, ICreateManyResult<TModel> result)
        {
            ExecuteManyOperation(UpdateMany, entities, result);
        }
        
        public void DeleteOne(TModel entity)
        {
            RequireExistingObject(entity);
            var filter = Builders<TModel>.Filter.Eq("_id", entity.ObjectId);
            var result = Collection.DeleteOne(filter);
            if (result.DeletedCount != 1)
                throw new UnexpectedResultException(entity.Id, "delete");
        }
        
        public void DeleteOne(TModel entity, IDeleteOneResult<TModel> result)
        {
            ExecuteOneOperation(DeleteOne, entity, result);
        }
        
        public void DeleteMany(IEnumerable<TModel> entities)
        {
            entities.ToList().ForEach(RequireExistingObject);
            var ids = from entity in entities select entity.ObjectId;
            var filter = Builders<TModel>.Filter.In("_id", ids);
            var result = Collection.DeleteMany(filter);
            if (result.DeletedCount != entities.Count())
                throw new UnexpectedResultException("all entities could not be deleted");
        }
        
        public void DeleteMany(IEnumerable<TModel> entities, ICreateManyResult<TModel> result)
        {
            ExecuteManyOperation(DeleteMany, entities, result);
        }
        
        public IFindOneResult<TModel> FindOne(string id, bool throwOnError = true)
        {
            try
            {
                if (!ValidateObjectId(id))
                    throw new InvalidIdException(id);
            }
            catch (Exception error)
            {
                if (throwOnError)
                    throw;
                return new FindOneResult<TModel>(error);
            }
            return FindOne(entity => entity.Id == id, throwOnError);
        }
        
        public IFindOneResult<TModel> FindOne(
            Expression<Func<TModel, bool>> filter,
            bool throwOnError = true)
        {
            try
            {
                var fluentObjects = Collection.Find(filter);
                var foundObjects = fluentObjects.Count();
                if (foundObjects > 1)
                    throw new ManyFoundException(typeof(TModel), filter.ToString());
                return new FindOneResult<TModel>(foundObjects == 1
                    ? fluentObjects.Single()
                    : null
                );
            }
            catch (Exception error)
            {
                if (throwOnError)
                    throw;
                return new FindOneResult<TModel>(error);
            }
        }
        
        public IFindManyResult<TModel> FindMany(
            Expression<Func<TModel, bool>> filter,
            bool throwOnError = true)
        {
            try
            {
                return new FindManyResult<TModel>(Collection.Find(filter));
            }
            catch (Exception error)
            {
                if (throwOnError)
                    throw;
                return new FindManyResult<TModel>(error);
            }
        }
    }
}
