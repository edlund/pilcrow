using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public interface IDeleteManyResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
