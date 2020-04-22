using Microsoft.EntityFrameworkCore.Migrations;

namespace Skyline.Infrastructure.Data.Migrations
{
    public partial class InitializeSkylineDbConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(maxLength: 64, nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    Province = table.Column<string>(maxLength: 32, nullable: true),
                    City = table.Column<string>(maxLength: 32, nullable: true),
                    Zip = table.Column<string>(maxLength: 10, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Status = table.Column<int>(nullable: false)
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
