using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Pilcrow.Db.Models;
using Pilcrow.Db.Repositories;

namespace Pilcrow.Tests.Helpers.Db
{
    public static class RepositoryHelper
    {
        public static List<T> CreateObjects<T>(int min, int max, Func<T> maker)
        {
            return (from _ in Enumerable.Range(min, max) select maker()).ToList();
        }
        
        public static List<T> CreateObjects<T>(int min, int max, Func<T> maker, IRepository<T> repository)
            where T : class, IEntity
        {
            var xs = CreateObjects<T>(min, max, maker);
            repository.CreateMany(xs);
            return xs;
        }
    }
}
