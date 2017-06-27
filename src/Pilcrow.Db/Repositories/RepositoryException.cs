
using System;

namespace Pilcrow.Db.Repositories
{
    public abstract class RepositoryException : Exception
    {
        protected RepositoryException(string message) : base(message)
        {}
        
        protected RepositoryException(string message, Exception inner) : base(message, inner)
        {}
    }
}
