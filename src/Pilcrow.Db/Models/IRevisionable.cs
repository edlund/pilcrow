
namespace Pilcrow.Db.Models
{
    public interface IRevisionable
    {
        int Version { get; set; }
    }
}
