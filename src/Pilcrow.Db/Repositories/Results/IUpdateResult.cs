using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public interface IUpdateResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
