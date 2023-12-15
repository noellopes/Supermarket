﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Supermarket.Data;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    [DbContext(typeof(SupermarketDbContext))]
    [Migration("20231215102351_Supplier")]
    partial class Supplier
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Supermarket.Models.Addresses", b =>
                {
                    b.Property<int>("AddressesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressesId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressesId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Supermarket.Models.Folga", b =>
                {
                    b.Property<int>("FolgaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FolgaId"));

                    b.Property<DateTime?>("DataFim")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataInicio")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataPedido")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gestor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FolgaId");

                    b.ToTable("Folga");
                });

            modelBuilder.Entity("Supermarket.Models.PurchaseSupplier", b =>
                {
                    b.Property<int>("PurchaseSupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseSupplierId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubTotal")
                        .HasColumnType("int");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("PurchaseSupplierId");

                    b.HasIndex("SupplierId");

                    b.ToTable("PurchaseSupplier");
                });

            modelBuilder.Entity("Supermarket.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadQuarters")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Telefone")
                        .HasColumnType("int");

                    b.HasKey("SupplierId");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Supermarket.Models.Addresses", b =>
                {
                    b.HasOne("Supermarket.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Supermarket.Models.PurchaseSupplier", b =>
                {
                    b.HasOne("Supermarket.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });
#pragma warning restore 612, 618
        }
    }
}
