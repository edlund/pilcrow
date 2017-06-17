
using System;

namespace Pilcrow.Db.Repositories.Exceptions
{
    public class IdIsNotNullOrEmptyException : RepositoryException
    {
        const string MessageTemplate = "Id should be null or Empty, but is \"{0}\"";
        
        public IdIsNotNullOrEmptyException(string id)
            : base(string.Format(MessageTemplate, id))
        {}
        
        public IdIsNotNullOrEmptyException(string id, Exception inner)
            : base(string.Format(MessageTemplate, id), inner)
        {}
    }
}
