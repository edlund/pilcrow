using System;
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
    }
    
    public abstract class Repository<TModel> : Repository, IRepository<TModel>
        where TModel : class, IEntity
    {
        protected readonly IMongoCollection<TModel> Collection;
        
        protected Repository(IContext context) : base(context)
        {
            Collection = Context.Database.GetCollection<TModel>(CollectionName(typeof(TModel)));
        }
        
        private void ExecuteOperation(
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
        
        private void RequireExistingObject(TModel entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(entity.Id))
                throw new IdIsNullOrEmptyException(entity.GetType());
            if (!ValidateObjectId(entity.Id))
                throw new InvalidIdException(entity.Id);
        }
        
        public string CollectionName(Type type)
        {
            return $"{type.FullName}".Replace('.', '_').ToLower();
        }
        
        public bool ValidateObjectId(string value)
        {
            ObjectId objectId;
            return ObjectId.TryParse(value, out objectId);
        }
        
        public void Create(TModel entity)
        {
            if (!string.IsNullOrEmpty(entity.Id))
                throw new IdIsNotNullOrEmptyException(entity.Id);
            if (entity.ModificationTime == DateTime.MinValue)
                entity.ModificationTime = DateTime.Now;
            Collection.InsertOne(entity);
        }
        
        public void Create(TModel entity, ICreateResult<TModel> result)
        {
            ExecuteOperation(Create, entity, result);
        }
        
        public void Update(TModel entity)
        {
            RequireExistingObject(entity);
            entity.ModificationTime = DateTime.Now;
            var filter = Builders<TModel>.Filter.Eq("_id", entity.ObjectId);
            var result = Collection.ReplaceOne(filter, entity);
            if (result.ModifiedCount != 1)
                throw new UnexpectedResultException(entity.Id, "update");
        }
        
        public void Update(TModel entity, IUpdateResult<TModel> result)
        {
            ExecuteOperation(Update, entity, result);
        }
        
        public void Delete(TModel entity)
        {
            RequireExistingObject(entity);
            var filter = Builders<TModel>.Filter.Eq("_id", entity.ObjectId);
            var result = Collection.DeleteOne(filter);
            if (result.DeletedCount != 1)
                throw new UnexpectedResultException(entity.Id, "delete");
        }
        
        public void Delete(TModel entity, IDeleteResult<TModel> result)
        {
            ExecuteOperation(Delete, entity, result);
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
