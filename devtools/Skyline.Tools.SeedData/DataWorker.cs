using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Skyline.Tools.SeedData.Data;
using Skyline.Tools.SeedData.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Dapper;

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

            int count = 1000 * 100;
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

        public async Task WorkWithDapper()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _logger.LogInformation("start seed data start - with dapper");
            var conn = Configuration.GetConnectionString("sqlite");

            int count = 1000 * 1000;
            int index = 0;
            using (var connection = new SqliteConnection(conn))
            {
                connection.Open();
                while (index < count)
                {
                    List<Tmp> list = new List<Tmp>();
                    StringBuilder sb = new StringBuilder();

                    var now = DateTime.Now;
                    var utcNow = DateTime.UtcNow;
                    var nowOffset = DateTimeOffset.Now;
                    sb.Append("insert into tmp (time,time_utc,time_offset,remark,is_delete) values ");
                    for (int i = 0; i < 10000; i++)
                    {
                        //sb.Append($"('{DateTime.Now}','{DateTime.UtcNow}','{DateTimeOffset.Now}','remark{i}',{(i % 3 == 0 ? 1 : 0)}),");
                        sb.AppendFormat("('{0}','{1}','{2}','{3}',(4)),", now, utcNow, nowOffset, "remark" + i, i % 3 == 0 ? 1 : 0);
                        //sb.Append("(");
                        //sb.Append($"'{DateTime.Now}',");
                        //sb.Append($"'{DateTime.UtcNow}',");
                        //sb.Append($"'{DateTimeOffset.Now}',");
                        //sb.Append($"'remark{i}',");
                        //sb.Append(i % 3 == 0 ? 1 : 0);
                        //sb.Append(");");

                        //list.Add(new Tmp
                        //{
                        //    is_delete = i % 3 == 0,
                        //    remark = $"remark{i}",
                        //    time = DateTime.Now,
                        //    time_offset = DateTimeOffset.Now,
                        //    time_utc = DateTime.UtcNow
                        //});


                        index++;
                    }
                    //await connection.ExecuteAsync("insert into tmp (time,time_utc,time_offset,remark,is_delete) values (@time,@time_utc,@time_offset,@remark,@is_delete)", list);
                    //await connection.InsertAsync(list); // take so long
                    var tmpInserSql = sb.ToString();
                    var insertSql = tmpInserSql.Substring(0, tmpInserSql.LastIndexOf(','));
                    await connection.ExecuteAsync(insertSql);
                    _logger.LogInformation($"{index} records inserted");
                }
            }
            stopwatch.Stop();
            _logger.LogInformation("seed data end - with dapper");
            _logger.LogInformation($"elapsed time:{stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
