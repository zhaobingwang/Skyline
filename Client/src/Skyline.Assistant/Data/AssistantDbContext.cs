using Microsoft.EntityFrameworkCore;
using Skyline.Assistant.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Skyline.Assistant.Data
{
    public class AssistantDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //builder.UseSqlite(ConfigurationManager.ConnectionStrings["AssistantDatabase"].ConnectionString);
            builder.UseSqlite(@"Data Source=C:\db\sqlite\skyline.assistant.db");
        }
        public DbSet<SecretEntity> Secrets { get; set; }
    }
}
