using Pilcrow.Db.Models;
using Pilcrow.Db.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pilcrow.Tests.Helpers
{
    public static class RepositoryHelper
    {
        public static List<T> CreateObjects<T>(int min, int max, IRepository<T> repository, Func<T> maker)
            where T : class, IEntity
        {
            var xs = (from _ in Enumerable.Range(min, max) select maker()).ToList();
            xs.ForEach(x => repository.Create(x));
            return xs;
        }
    }
}