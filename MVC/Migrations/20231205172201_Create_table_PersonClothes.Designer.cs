﻿// <auto-generated />
using System;
using MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231205172201_Create_table_PersonClothes")]
    partial class Create_table_PersonClothes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("MVC.Models.Book", b =>
                {
                    b.Property<int?>("IdBook")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameBook")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NhaXuatBan")
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdBook");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("MVC.Models.Clothes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClothesID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ClothesName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clothes");
                });

            modelBuilder.Entity("MVC.Models.SinhVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("BorrowDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("IdBook")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("IdSV")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Khoa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NameSV")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("PayDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneSV")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SinhVien");
                });

            modelBuilder.Entity("MVC.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
