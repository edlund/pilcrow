
using System;

namespace Pilcrow.Db.Repositories.Exceptions
{
    public class InvalidIdException : RepositoryException
    {
        const string MessageTemplate = "\"{0}\" is not a valid ObjectId";
        
        public InvalidIdException(string id)
            : base(string.Format(MessageTemplate, id))
        {}
        
        public InvalidIdException(string id, Exception inner)
            : base(string.Format(MessageTemplate, id))
        {}
    }
}
