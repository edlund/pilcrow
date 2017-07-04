
namespace Pilcrow.Db.Models
{
    /// <summary>
    /// Have models implement this interface if they can be
    /// AutoMapped by MongoDb.Driver. If this is not an option,
    /// the mapping must be handled manually during Startup.
    /// </summary>
    public interface IAutoMappable
    {
    }
}
