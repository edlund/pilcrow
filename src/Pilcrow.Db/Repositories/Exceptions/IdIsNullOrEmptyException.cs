
using System;

namespace Pilcrow.Db.Repositories.Exceptions
{
    public class IdIsNullOrEmptyException : RepositoryException
    {
        const string MessageTemplate = "Id for \"{0}\" is null or Empty";
        
        public IdIsNullOrEmptyException(Type type)
            : base(string.Format(MessageTemplate, type.FullName))
        {}
        
        public IdIsNullOrEmptyException(Type type, Exception inner)
            : base(string.Format(MessageTemplate, type.FullName), inner)
        {}
    }
}
