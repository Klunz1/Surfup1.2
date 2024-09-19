﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurfsupEmil.Models;

#nullable disable

namespace SurfsupEmil.Migrations
{
    [DbContext(typeof(SurfsUpDbContext))]
    [Migration("20240919124150_NotMappedOrder")]
    partial class NotMappedOrder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SurfsupEmil.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime?>("OrderDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PickupDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SurfsupEmil.Models.Surfboard", b =>
                {
                    b.Property<int>("SurfboardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SurfboardId"));

                    b.Property<string>("Equipment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("HourlyPrice")
                        .HasColumnType("float");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("PriceOfPurchase")
                        .HasColumnType("float");

                    b.Property<double>("Thickness")
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<double>("Volume")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("SurfboardId");

                    b.HasIndex("OrderId");

                    b.ToTable("Surfboards");
                });

            modelBuilder.Entity("SurfsupEmil.Models.Surfboard", b =>
                {
                    b.HasOne("SurfsupEmil.Models.Order", null)
                        .WithMany("Surfboards")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("SurfsupEmil.Models.Order", b =>
                {
                    b.Navigation("Surfboards");
                });
#pragma warning restore 612, 618
        }
    }
}