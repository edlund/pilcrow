using Pilcrow.Db.Repositories.Cms;

namespace Pilcrow.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        
        public PageService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
    }
}
