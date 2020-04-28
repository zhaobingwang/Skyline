using Microsoft.EntityFrameworkCore;
using Skyline.Tools.SeedData.Data.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Skyline.Tools.SeedData.Data
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }
        public DbSet<Tmp> Tmp { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
