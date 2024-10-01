﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurfsupEmil.Models;

#nullable disable

namespace SurfsupEmil.Migrations
{
    [DbContext(typeof(SurfsUpDbContext))]
    partial class SurfsUpDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderSurfboard", b =>
                {
                    b.Property<int>("OrdersOrderId")
                        .HasColumnType("int");

                    b.Property<int>("SurfboardsSurfboardId")
                        .HasColumnType("int");

                    b.HasKey("OrdersOrderId", "SurfboardsSurfboardId");

                    b.HasIndex("SurfboardsSurfboardId");

                    b.ToTable("OrderSurfboard", (string)null);
                });

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

                    b.ToTable("Orders", (string)null);
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

                    b.Property<double>("PriceOfPurchase")
                        .HasColumnType("float");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<double>("Thickness")
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<double>("Volume")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("SurfboardId");

                    b.ToTable("Surfboards", (string)null);
                });

            modelBuilder.Entity("OrderSurfboard", b =>
                {
                    b.HasOne("SurfsupEmil.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SurfsupEmil.Models.Surfboard", null)
                        .WithMany()
                        .HasForeignKey("SurfboardsSurfboardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
