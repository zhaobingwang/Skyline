using Microsoft.EntityFrameworkCore.Migrations;

namespace Skyline.Infrastructure.Migrations.ApplicationDb
{
    public partial class AddFieldsStatusForContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Contacts",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Contacts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contacts");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Contacts",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);
        }
    }
}
