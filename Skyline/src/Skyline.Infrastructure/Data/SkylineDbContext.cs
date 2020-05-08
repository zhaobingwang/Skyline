using Microsoft.EntityFrameworkCore;
using Skyline.ApplicationCore.Entities.ContactAggregate;
using System.Reflection;

namespace Skyline.Infrastructure.Data
{
    public class SkylineDbContext : DbContext
    {
        public SkylineDbContext(DbContextOptions<SkylineDbContext> options) : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
