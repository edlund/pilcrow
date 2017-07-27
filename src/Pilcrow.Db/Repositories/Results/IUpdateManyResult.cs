using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public interface IUpdateManyResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
