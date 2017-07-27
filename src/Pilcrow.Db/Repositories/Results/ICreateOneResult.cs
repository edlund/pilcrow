using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public interface ICreateOneResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
