using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Skyline.Console.Infrastructure.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, comment: "Id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "图标编码"),
                    Size = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "图标大小(px)"),
                    Color = table.Column<string>(type: "nvarchar(50)", nullable: true, comment: "图标颜色"),
                    Description = table.Column<string>(type: "nvarchar(1024)", nullable: true, comment: "图标说明"),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserGuid = table.Column<Guid>(nullable: false),
                    CreateUserLoginName = table.Column<string>(nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: false),
                    LastModifyUserGuid = table.Column<Guid>(nullable: false),
                    LastModifyUserLoginName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false, defaultValueSql: "newid()", comment: "Guid"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "菜单名称"),
                    Url = table.Column<string>(type: "nvarchar(128)", nullable: true, comment: " 菜单图标(可选)"),
                    Alias = table.Column<string>(type: "nvarchar(255)", nullable: true, comment: "页面别名"),
                    Icon = table.Column<string>(nullable: true),
                    ParentGuid = table.Column<Guid>(nullable: false),
                    ParentName = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: true, comment: "描述信息"),
                    Sort = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    IsDefaultRouter = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserGuid = table.Column<Guid>(nullable: false),
                    CreateUserLoginName = table.Column<string>(nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: false),
                    LastModifyUserGuid = table.Column<Guid>(nullable: false),
                    LastModifyUserLoginName = table.Column<string>(nullable: true),
                    HideMenu = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(32)", nullable: false, comment: "角色编码"),
                    Name = table.Column<string>(type: "nvarchar(32)", nullable: false, comment: "角色名称"),
                    Description = table.Column<string>(type: "nvarchar(1024)", nullable: true, comment: "角色描述"),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserGuidId = table.Column<Guid>(nullable: false),
                    CreateUserName = table.Column<string>(nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ModifiyUserId = table.Column<Guid>(nullable: false),
                    ModifyUserName = table.Column<string>(nullable: true),
                    IsSuperAdministrator = table.Column<bool>(nullable: false),
                    Builtin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false, defaultValueSql: "newid()", comment: "Guid"),
                    LoginName = table.Column<string>(type: "nvarchar(16)", nullable: false, comment: "登录账户名"),
                    NickName = table.Column<string>(type: "nvarchar(16)", nullable: true, comment: "昵称"),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", nullable: true, comment: "密码哈希值"),
                    Avatar = table.Column<string>(type: "nvarchar(255)", nullable: true, comment: "头像"),
                    DOB = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    MyProperty = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: false),
                    CreateUserName = table.Column<string>(nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: false),
                    LastModifyUserId = table.Column<string>(nullable: true),
                    LastModifyUserName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: true, comment: "描述信息")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(20)", nullable: false, comment: "权限编码"),
                    MenuGuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "权限编码"),
                    ActionCode = table.Column<string>(type: "nvarchar(80)", nullable: false, comment: "权限编码"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "描述信息"),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserGuid = table.Column<Guid>(nullable: false),
                    CreateUserLoginName = table.Column<string>(nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: false),
                    LastModifyUserGuid = table.Column<Guid>(nullable: false),
                    LastModifyUserLoginName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Permissions_Menus_MenuGuid",
                        column: x => x.MenuGuid,
                        principalTable: "Menus",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserGuid = table.Column<Guid>(nullable: false),
                    RoleCode = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserGuid, x.RoleCode });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleCode",
                        column: x => x.RoleCode,
                        principalTable: "Roles",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserGuid",
                        column: x => x.UserGuid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleCode = table.Column<string>(nullable: false),
                    PermissionCode = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleCode, x.PermissionCode });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionCode",
                        column: x => x.PermissionCode,
                        principalTable: "Permissions",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleCode",
                        column: x => x.RoleCode,
                        principalTable: "Roles",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Code",
                table: "Permissions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_MenuGuid",
                table: "Permissions",
                column: "MenuGuid");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionCode",
                table: "RolePermissions",
                column: "PermissionCode");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Code",
                table: "Roles",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleCode",
                table: "UserRoles",
                column: "RoleCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
