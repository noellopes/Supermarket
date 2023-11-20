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
    [Migration("20231120112232_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
#pragma warning restore 612, 618
        }
    }
}
