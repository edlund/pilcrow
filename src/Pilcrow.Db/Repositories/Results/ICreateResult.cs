using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public interface ICreateResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
