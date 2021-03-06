﻿// <auto-generated />
using System;
using CRUDApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRUDApi.Migrations.AuthDb
{
    [DbContext(typeof(AuthDbContext))]
    [Migration("20201207035951_authDB")]
    partial class authDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CRUDApi.Models.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreateByIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("expireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("relaceById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("revokedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("RefeshToken");
                });

            modelBuilder.Entity("CRUDApi.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usename")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CRUDApi.Models.RefreshToken", b =>
                {
                    b.HasOne("CRUDApi.Models.User", "User")
                        .WithMany("RefeshTokens")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("CRUDApi.Models.User", b =>
                {
                    b.Navigation("RefeshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
