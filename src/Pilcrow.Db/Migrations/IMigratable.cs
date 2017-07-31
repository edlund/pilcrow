
using Microsoft.Extensions.Configuration;

namespace Pilcrow.Db.Migrations
{
    public interface IMigratable
    {
        IContext Context { get; set; }
        
        IConfigurationRoot Configuration { get; set; }
        
        void Execute();
    }
}
