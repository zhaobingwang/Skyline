﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skyline.Tools.SeedData.Data;

namespace Skyline.Tools.SeedData.Data.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20200428142524_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("Skyline.Tools.SeedData.Data.Entities.Tmp", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("is_delete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("remark")
                        .HasColumnType("TEXT")
                        .HasMaxLength(1024);

                    b.Property<DateTime>("time")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("time_offset")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("time_utc")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("tmp");
                });
#pragma warning restore 612, 618
        }
    }
}
