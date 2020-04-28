using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Skyline.Tools.SeedData.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EFContext>
    {
        public EFContext CreateDbContext(string[] args)
        {
            var dir = "c:/opt/skyline/db";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var optionsBuilder = new DbContextOptionsBuilder<EFContext>();
            optionsBuilder.UseSqlite($"Data Source={dir}/skyline.tmp.db");

            return new EFContext(optionsBuilder.Options);
        }
    }
}
