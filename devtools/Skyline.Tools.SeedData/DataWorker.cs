using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Skyline.Tools.SeedData.Data;
using Skyline.Tools.SeedData.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Tools.SeedData
{
    public class DataWorker
    {
        public readonly ILogger _logger;
        private readonly IConfiguration Configuration;
        private readonly EFContext db;
        public DataWorker(ILogger<DataWorker> logger, IConfiguration configuration, EFContext dbContext)
        {
            _logger = logger;
            Configuration = configuration;
            db = dbContext;
        }
        public async Task Work()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _logger.LogInformation("start seed data");
            var conn = Configuration.GetConnectionString("sqlite");

            int count = 1000 * 1000;
            int index = 0;
            while (index < count)
            {
                List<Tmp> list = new List<Tmp>();
                for (int i = 0; i < 1000; i++)
                {
                    list.Add(new Tmp
                    {
                        is_delete = i % 3 == 0,
                        remark = $"remark{i}",
                        time = DateTime.Now,
                        time_offset = DateTimeOffset.Now,
                        time_utc = DateTime.UtcNow
                    });
                    index++;
                }
                db.Tmp.AddRange(list);
                await db.SaveChangesAsync();
                _logger.LogInformation($"{index} records inserted");
            }
            stopwatch.Stop();
            _logger.LogInformation("seed data end");
            _logger.LogInformation($"elapsed time:{stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
