using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Skyline.Infrastructure.Migrations.ApplicationDb
{
    public partial class InitApplicationDbConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<string>(maxLength: 32, nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    Province = table.Column<string>(maxLength: 32, nullable: true),
                    City = table.Column<string>(maxLength: 32, nullable: true),
                    Zip = table.Column<string>(maxLength: 10, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
