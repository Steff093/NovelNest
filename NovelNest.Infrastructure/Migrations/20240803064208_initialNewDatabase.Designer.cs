﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NovelNest.Infrastructure.Database;

#nullable disable

namespace NovelNest.Infrastructure.Migrations
{
    [DbContext(typeof(NovelNestDataContext))]
    [Migration("20240803064208_initialNewDatabase")]
    partial class initialNewDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NovelNest.Domain.Entities.BookEntities.BookEntity", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FolderID")
                        .HasColumnType("int");

                    b.Property<byte[]>("ImageBook")
                        .HasMaxLength(5242880)
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageMIMEType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsPictureAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.HasIndex("FolderID");

                    b.ToTable("BookEntities");
                });

            modelBuilder.Entity("NovelNest.Domain.Entities.FolderEntities.FolderEntity", b =>
                {
                    b.Property<int>("FolderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FolderID"));

                    b.Property<string>("FolderDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FolderName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("FolderID");

                    b.ToTable("FolderEntities");
                });

            modelBuilder.Entity("NovelNest.UI.Domain.Entities.LoginEntities.UserEntity", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varbinary(255)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserID");

                    b.ToTable("UserEntities");
                });

            modelBuilder.Entity("NovelNest.Domain.Entities.BookEntities.BookEntity", b =>
                {
                    b.HasOne("NovelNest.Domain.Entities.FolderEntities.FolderEntity", "Folder")
                        .WithMany("BookEntities")
                        .HasForeignKey("FolderID");

                    b.Navigation("Folder");
                });

            modelBuilder.Entity("NovelNest.Domain.Entities.FolderEntities.FolderEntity", b =>
                {
                    b.Navigation("BookEntities");
                });
#pragma warning restore 612, 618
        }
    }
}