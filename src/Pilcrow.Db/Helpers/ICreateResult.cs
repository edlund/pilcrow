
using Pilcrow.Db.Models;

namespace Pilcrow.Db.Helpers
{
    public interface ICreateResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
