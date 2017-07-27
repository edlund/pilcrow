using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public interface ICreateManyResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
