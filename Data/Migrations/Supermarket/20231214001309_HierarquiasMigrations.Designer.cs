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
    [Migration("20231214001309_HierarquiasMigrations")]
    partial class HierarquiasMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Supermarket.Models.Card_Movement", b =>
                {
                    b.Property<int>("Card_Id")
                        .HasColumnType("int");

                    b.Property<int>("Movement_Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Movement_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Card_Id", "Movement_Id");

                    b.ToTable("Card_Movement");
                });

            modelBuilder.Entity("Supermarket.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Employee_Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Employee_Admission_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Employee_Birth_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Employee_Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Employee_NIF")
                        .HasMaxLength(9)
                        .HasColumnType("int");

                    b.Property<string>("Employee_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Employee_Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Employee_Phone")
                        .HasMaxLength(9)
                        .HasColumnType("int");

                    b.Property<DateTime>("Employee_Termination_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Employee_Time_Bank")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Hora_Almoco_Padrao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Standard_Check_In_Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Standard_Check_Out_Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Standard_Lunch_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("EmployeeId");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("Supermarket.Models.EmployeeEvaluation", b =>
                {
                    b.Property<int>("EmployeeEvaluationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeEvaluationId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("GradeNumber")
                        .HasColumnType("int");

                    b.HasKey("EmployeeEvaluationId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AvaliacaoFuncionarios");
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

            modelBuilder.Entity("Supermarket.Models.Funcoes", b =>
                {
                    b.Property<int>("IdFuncao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFuncao"));

                    b.Property<string>("DescricaoFuncao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NomeFuncao")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("IdFuncao");

                    b.ToTable("Funcoes");
                });

            modelBuilder.Entity("Supermarket.Models.HierarquiasModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hierarquias");
                });

            modelBuilder.Entity("Supermarket.Models.Meal_Card", b =>
                {
                    b.Property<int>("Card_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Card_Id"));

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("Card_Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("Meal_Card");
                });

            modelBuilder.Entity("Supermarket.Models.Card_Movement", b =>
                {
                    b.HasOne("Supermarket.Models.Meal_Card", "Meal_Card")
                        .WithMany("Movements")
                        .HasForeignKey("Card_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal_Card");
                });

            modelBuilder.Entity("Supermarket.Models.EmployeeEvaluation", b =>
                {
                    b.HasOne("Supermarket.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Supermarket.Models.Meal_Card", b =>
                {
                    b.HasOne("Supermarket.Models.Employee", "Employee")
                        .WithOne("Meal_Card")
                        .HasForeignKey("Supermarket.Models.Meal_Card", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Supermarket.Models.Employee", b =>
                {
                    b.Navigation("Meal_Card")
                        .IsRequired();
                });

            modelBuilder.Entity("Supermarket.Models.Meal_Card", b =>
                {
                    b.Navigation("Movements");
                });
#pragma warning restore 612, 618
        }
    }
}
