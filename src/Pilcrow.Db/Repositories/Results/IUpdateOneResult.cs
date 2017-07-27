using Pilcrow.Db.Models;

namespace Pilcrow.Db.Repositories.Results
{
    public interface IUpdateOneResult<TModel>: IOperationResult<TModel>
        where TModel : class, IEntity
    {
    }
}
