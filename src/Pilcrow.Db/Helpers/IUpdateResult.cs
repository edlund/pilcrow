
using Pilcrow.Db.Models;

namespace Pilcrow.Db.Helpers
{
    public interface IUpdateResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
