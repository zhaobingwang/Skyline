using System.Threading.Tasks;

namespace Skyline.Data
{
    public interface ISkylineDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
