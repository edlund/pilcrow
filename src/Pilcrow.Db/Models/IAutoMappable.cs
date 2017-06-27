
namespace Pilcrow.Db.Models
{
    /// <summary>
    /// Have models implement this interface if they can not be
    /// AutoMapped by MongoDb.Driver. In that case the mapping
    /// must be handled manually during Startup.
    /// </summary>
    public interface IAutoMappable
    {
    }
}
