using Microsoft.EntityFrameworkCore;
using Skyline.Assistant.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Skyline.Assistant.Infrastructure.Data
{
    public class AssistantSqliteDbContext : DbContext
    {
        public DbSet<SecretEntity> Secrets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //builder.UseSqlite(ConfigurationManager.ConnectionStrings["SqliteConncetion"].ConnectionString);
            builder.UseSqlite(@"Data Source=C:\db\sqlite\Skyline.Assistant.db");
        }
    }
}
