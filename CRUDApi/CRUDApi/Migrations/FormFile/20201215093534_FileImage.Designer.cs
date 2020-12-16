﻿// <auto-generated />
using System;
using CRUDApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRUDApi.Migrations.FormFile
{
    [DbContext(typeof(FormFileContext))]
    [Migration("20201215093534_FileImage")]
    partial class FileImage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CRUDApi.Models.Images.ImageModel", b =>
                {
                    b.Property<int>("FormFileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Usename")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FormFileID");

                    b.ToTable("FileDB");
                });
#pragma warning restore 612, 618
        }
    }
}
