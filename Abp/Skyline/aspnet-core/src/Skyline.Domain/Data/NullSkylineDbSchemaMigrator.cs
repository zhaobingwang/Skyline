using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Skyline.Data
{
    /* This is used if database provider does't define
     * ISkylineDbSchemaMigrator implementation.
     */
    public class NullSkylineDbSchemaMigrator : ISkylineDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}