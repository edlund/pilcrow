
using System;

namespace Pilcrow.Db.Repositories.Exceptions
{
    public class UnexpectedResultException : RepositoryException
    {
        const string MessageTemplate = "failed to {1} Object with Id \"{0}\"";
        
        public UnexpectedResultException(string id, string operation)
            : base(string.Format(MessageTemplate, id, operation))
        {}
        
        public UnexpectedResultException(string id, string operation, Exception inner)
            : base(string.Format(MessageTemplate, id, operation), inner)
        {}
    }
}
