using Pilcrow.Db.Models.Cms;

namespace Pilcrow.Db.Repositories.Cms
{
    public interface IPageRepository : IRepository<Page>
    {
    }
    
    public class PageRepository : Repository<Page>, IPageRepository
    {
        public PageRepository(IContext context) : base(context)
        {}
    }
}
