using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Skyline.Tools.SeedData.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tmp",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    time = table.Column<DateTime>(nullable: false),
                    time_utc = table.Column<DateTime>(nullable: false),
                    time_offset = table.Column<DateTimeOffset>(nullable: false),
                    remark = table.Column<string>(maxLength: 1024, nullable: true),
                    is_delete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tmp", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tmp");
        }
    }
}
