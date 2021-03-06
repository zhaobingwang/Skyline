﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skyline.Console.Infrastructure.Data;

namespace Skyline.Console.Infrastructure.Data.Migrations
{
    [DbContext(typeof(SkylineDbContext))]
    partial class SkylineDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Skyline.Console.ApplicationCore.Entities.Icon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasComment("图标编码");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(50)")
                        .HasComment("图标颜色");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreateUserLoginName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1024)")
                        .HasComment("图标说明");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifyTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LastModifyUserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifyUserLoginName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(20)")
                        .HasComment("图标大小(px)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Icons");
                });

            modelBuilder.Entity("Skyline.Console.ApplicationCore.Entities.Menu", b =>
                {
                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()")
                        .HasComment("Guid");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(255)")
                        .HasComment("页面别名");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreateUserLoginName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasComment("描述信息");

                    b.Property<int?>("HideMenu")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsDefaultRouter")
                        .HasColumnType("int");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifyTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LastModifyUserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifyUserLoginName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasComment("菜单名称");

                    b.Property<Guid>("ParentGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ParentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(128)")
                        .HasComment(" 菜单图标(可选)");

                    b.HasKey("Guid");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Skyline.Console.ApplicationCore.Entities.Permission", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(20)")
                        .HasComment("权限编码");

                    b.Property<string>("ActionCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasComment("权限编码");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreateUserLoginName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("描述信息");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifyTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LastModifyUserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifyUserLoginName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MenuGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasComment("权限编码");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Code");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("MenuGuid");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Skyline.Console.ApplicationCore.Entities.Role", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(32)")
                        .HasComment("角色编码");

                    b.Property<bool>("Builtin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserGuidId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1024)")
                        .HasComment("角色描述");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<bool>("IsSuperAdministrator")
                        .HasColumnType("bit");

                    b.Property<Guid>("ModifiyUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)")
                        .HasComment("角色名称");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Code");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Skyline.Console.ApplicationCore.Entities.RolePermissionMapping", b =>
                {
                    b.Property<string>("RoleCode")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PermissionCode")
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("RoleCode", "PermissionCode");

                    b.HasIndex("PermissionCode");

                    b.ToTable("RolePermissionMappings");
                });

            modelBuilder.Entity("Skyline.Console.ApplicationCore.Entities.User", b =>
                {
                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()")
                        .HasComment("Guid");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(255)")
                        .HasComment("头像");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("date")
                        .HasComment("出生日期");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(255)")
                        .HasComment("描述信息");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifyTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LastModifyUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifyUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)")
                        .HasComment("登录账户名");

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(16)")
                        .HasComment("昵称");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasComment("密码哈希值");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Guid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Skyline.Console.ApplicationCore.Entities.UserRoleMapping", b =>
                {
                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleCode")
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("UserGuid", "RoleCode");

                    b.HasIndex("RoleCode");

                    b.ToTable("UserRoleMappings");
                });

            modelBuilder.Entity("Skyline.Console.ApplicationCore.Entities.Permission", b =>
                {
                    b.HasOne("Skyline.Console.ApplicationCore.Entities.Menu", "Menu")
                        .WithMany("Permissions")
                        .HasForeignKey("MenuGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Skyline.Console.ApplicationCore.Entities.RolePermissionMapping", b =>
                {
                    b.HasOne("Skyline.Console.ApplicationCore.Entities.Permission", "Permission")
                        .WithMany("Roles")
                        .HasForeignKey("PermissionCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Skyline.Console.ApplicationCore.Entities.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Skyline.Console.ApplicationCore.Entities.UserRoleMapping", b =>
                {
                    b.HasOne("Skyline.Console.ApplicationCore.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Skyline.Console.ApplicationCore.Entities.User", "User")
                        .WithMany("UserRoleMappings")
                        .HasForeignKey("UserGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
