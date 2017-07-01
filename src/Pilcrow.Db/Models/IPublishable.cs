
namespace Pilcrow.Db.Models
{
    public interface IPublishable
    {
        string Uuid { get; set; }
        
        bool Live { get; set; }
    }
}
