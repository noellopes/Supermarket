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
    [Migration("20231129012623_Issues")]
    partial class Issues
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

            modelBuilder.Entity("Supermarket.Models.IssueType", b =>
                {
                    b.Property<int>("IssueTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IssueTypeId"));

                    b.Property<string>("IssueDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IssueTypeId1")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IssueTypeId");

                    b.HasIndex("IssueTypeId1");

                    b.ToTable("IssueType");
                });

            modelBuilder.Entity("Supermarket.Models.Issues", b =>
                {
                    b.Property<int>("IssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IssueId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IssueRegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IssueTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Severity")
                        .HasColumnType("int");

                    b.HasKey("IssueId");

                    b.HasIndex("IssueTypeId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("Supermarket.Models.IssueType", b =>
                {
                    b.HasOne("Supermarket.Models.IssueType", null)
                        .WithMany("IssueTypes")
                        .HasForeignKey("IssueTypeId1");
                });

            modelBuilder.Entity("Supermarket.Models.Issues", b =>
                {
                    b.HasOne("Supermarket.Models.IssueType", "IssueType")
                        .WithMany()
                        .HasForeignKey("IssueTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IssueType");
                });

            modelBuilder.Entity("Supermarket.Models.IssueType", b =>
                {
                    b.Navigation("IssueTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
