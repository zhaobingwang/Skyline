using Microsoft.EntityFrameworkCore;
using Skyline.Assistant.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Skyline.Assistant.Infrastructure.Data
{
    public class AssistantDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite(ConfigurationManager.ConnectionStrings["AssistantDatabase"].ConnectionString);
            //builder.UseSqlite(@"Data Source=C:\db\sqlite\skyline.assistant.db");
        }
        public DbSet<Secret> Secrets { get; set; }
    }
}
