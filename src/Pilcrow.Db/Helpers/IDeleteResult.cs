
using Pilcrow.Db.Models;

namespace Pilcrow.Db.Helpers
{
    public interface IDeleteResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
