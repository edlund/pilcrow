
using System;

namespace Pilcrow.Db.Repositories.Exceptions
{
    public class ManyFoundException : RepositoryException
    {
        const string MessageTemplate = "many Objects of type {0} found with filter {1}";
        
        public ManyFoundException(Type type, string filter)
            : base(string.Format(MessageTemplate, type.FullName, filter))
        {}
        
        public ManyFoundException(Type type, string filter, Exception inner)
            : base(string.Format(MessageTemplate, type.FullName, filter), inner)
        {}
    }
}
