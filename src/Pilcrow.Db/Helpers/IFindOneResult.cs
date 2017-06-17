
using Pilcrow.Db.Models;

namespace Pilcrow.Db.Helpers
{
    public interface IFindOneResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
        TModel Object { get; }
    }
}
