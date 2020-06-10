using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Skyline.EntityFrameworkCore
{
    public static class SkylineDbContextModelCreatingExtensions
    {
        public static void ConfigureSkyline(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(SkylineConsts.DbTablePrefix + "YourEntities", SkylineConsts.DbSchema);

            //    //...
            //});
        }
    }
}