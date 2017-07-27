using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public interface IDeleteOneResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
