using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public interface IDeleteResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
