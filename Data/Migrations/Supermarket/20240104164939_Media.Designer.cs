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
    [Migration("20240104164939_Media")]
    partial class Media
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Supermarket.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("BrandId");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("Supermarket.Models.CardMovement", b =>
                {
                    b.Property<int>("CardMovementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CardMovementId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MealCardId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Movement_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("CardMovementId");

                    b.HasIndex("MealCardId");

                    b.ToTable("CardMovement");
                });

            modelBuilder.Entity("Supermarket.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Supermarket.Models.CategoryDiscount", b =>
                {
                    b.Property<int>("CategoryDiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryDiscountId"));

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryDiscountId");

                    b.ToTable("CategoryDiscounts");
                });

            modelBuilder.Entity("Supermarket.Models.ClientCard", b =>
                {
                    b.Property<int>("ClientCard_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientCard_Id"));

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<string>("ClientCard_Number")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.HasKey("ClientCard_Id");

                    b.ToTable("ClientCard");
                });

            modelBuilder.Entity("Supermarket.Models.Departments", b =>
                {
                    b.Property<int>("IDDepartments")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDDepartments"));

                    b.Property<string>("DescriptionDepartments")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NameDepartments")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("QuatDepMed")
                        .HasColumnType("int");

                    b.Property<string>("SkillsDepartments")
                        .IsRequired()
                        .HasMaxLength(155)
                        .HasColumnType("nvarchar(155)");

                    b.Property<bool>("StateDepartments")
                        .HasColumnType("bit");

                    b.HasKey("IDDepartments");

                    b.ToTable("Departments");
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

                    b.Property<string>("Employee_NIF")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Employee_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Employee_Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Employee_Phone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<DateTime>("Employee_Termination_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Employee_Time_Bank")
                        .HasColumnType("datetime2");

                    b.Property<string>("Standard_Check_In_Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Standard_Check_Out_Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Standard_Lunch_Hour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Standard_Lunch_Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("Supermarket.Models.Funcao", b =>
                {
                    b.Property<int>("FuncaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FuncaoId"));

                    b.Property<string>("DescricaoFuncao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NomeFuncao")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("FuncaoId");

                    b.ToTable("Funcao");
                });

            modelBuilder.Entity("Supermarket.Models.Hallway", b =>
                {
                    b.Property<int>("HallwayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HallwayId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("HallwayId");

                    b.HasIndex("StoreId");

                    b.ToTable("Hallway");
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

            modelBuilder.Entity("Supermarket.Models.MealCard", b =>
                {
                    b.Property<int>("MealCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealCardId"));

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("MealCardId");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("MealCard");
                });

            modelBuilder.Entity("Supermarket.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("MinimumQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalQuantity")
                        .HasColumnType("int");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Supermarket.Models.ProductDiscount", b =>
                {
                    b.Property<int>("ProductDiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductDiscountId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("ProductDiscountId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductDiscount");
                });

            modelBuilder.Entity("Supermarket.Models.ProductExpiration", b =>
                {
                    b.Property<int>("BatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BatchId"));

                    b.Property<string>("BatchNumber")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("BatchId");

                    b.ToTable("ProductExpiration");
                });

            modelBuilder.Entity("Supermarket.Models.ReduceProduct", b =>
                {
                    b.Property<int>("ReduceProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReduceProductId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int?>("ShelfId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WarehouseSectionId")
                        .HasColumnType("int");

                    b.HasKey("ReduceProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShelfId");

                    b.HasIndex("WarehouseSectionId");

                    b.ToTable("ReduceProduct");
                });

            modelBuilder.Entity("Supermarket.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleId"));

                    b.Property<DateTime>("DailyFinishTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DailyStartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IDDepartments")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ScheduleId");

                    b.HasIndex("IDDepartments");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("Supermarket.Models.Shelf", b =>
                {
                    b.Property<int>("ShelfId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShelfId"));

                    b.Property<int>("HallwayId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ShelfId");

                    b.HasIndex("HallwayId");

                    b.ToTable("Shelf");
                });

            modelBuilder.Entity("Supermarket.Models.Shelft_ProductExhibition", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ShelfId")
                        .HasColumnType("int");

                    b.Property<int>("MinimumQuantity")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "ShelfId");

                    b.HasIndex("ShelfId");

                    b.ToTable("Shelft_ProductExhibition");
                });

            modelBuilder.Entity("Supermarket.Models.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("StoreId");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("Supermarket.Models.Tickets", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<DateTime>("DataAtendimento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataEmissao")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentsIDDepartments")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int>("IDDepartments")
                        .HasColumnType("int");

                    b.Property<int>("NumeroDaSenha")
                        .HasColumnType("int");

                    b.Property<bool>("Prioritario")
                        .HasColumnType("bit");

                    b.HasKey("TicketId");

                    b.HasIndex("DepartmentsIDDepartments");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Supermarket.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarehouseId"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("Supermarket.Models.WarehouseSection", b =>
                {
                    b.Property<int>("WarehouseSectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarehouseSectionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("WarehouseSectionId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseSection");
                });

            modelBuilder.Entity("Supermarket.Models.WarehouseSection_Product", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseSectionId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ReservedQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "WarehouseSectionId");

                    b.HasIndex("WarehouseSectionId");

                    b.ToTable("WarehouseSection_Product");
                });

            modelBuilder.Entity("Supermarket.Models.CardMovement", b =>
                {
                    b.HasOne("Supermarket.Models.MealCard", "MealCard")
                        .WithMany("CardMovements")
                        .HasForeignKey("MealCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealCard");
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

            modelBuilder.Entity("Supermarket.Models.Hallway", b =>
                {
                    b.HasOne("Supermarket.Models.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
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

            modelBuilder.Entity("Supermarket.Models.MealCard", b =>
                {
                    b.HasOne("Supermarket.Models.Employee", "Employee")
                        .WithOne("MealCard")
                        .HasForeignKey("Supermarket.Models.MealCard", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Supermarket.Models.Product", b =>
                {
                    b.HasOne("Supermarket.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Supermarket.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Supermarket.Models.ProductDiscount", b =>
                {
                    b.HasOne("Supermarket.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Supermarket.Models.ReduceProduct", b =>
                {
                    b.HasOne("Supermarket.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Supermarket.Models.Shelf", "Shelf")
                        .WithMany()
                        .HasForeignKey("ShelfId");

                    b.HasOne("Supermarket.Models.WarehouseSection", "WarehouseSection")
                        .WithMany()
                        .HasForeignKey("WarehouseSectionId");

                    b.Navigation("Product");

                    b.Navigation("Shelf");

                    b.Navigation("WarehouseSection");
                });

            modelBuilder.Entity("Supermarket.Models.Schedule", b =>
                {
                    b.HasOne("Supermarket.Models.Departments", "Departments")
                        .WithMany()
                        .HasForeignKey("IDDepartments")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departments");
                });

            modelBuilder.Entity("Supermarket.Models.Shelf", b =>
                {
                    b.HasOne("Supermarket.Models.Hallway", "Hallway")
                        .WithMany()
                        .HasForeignKey("HallwayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hallway");
                });

            modelBuilder.Entity("Supermarket.Models.Shelft_ProductExhibition", b =>
                {
                    b.HasOne("Supermarket.Models.Product", "Product")
                        .WithMany("Shelf")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Supermarket.Models.Shelf", "Shelf")
                        .WithMany("Product")
                        .HasForeignKey("ShelfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("Supermarket.Models.Tickets", b =>
                {
                    b.HasOne("Supermarket.Models.Departments", "Departments")
                        .WithMany()
                        .HasForeignKey("DepartmentsIDDepartments");

                    b.Navigation("Departments");
                });

            modelBuilder.Entity("Supermarket.Models.WarehouseSection", b =>
                {
                    b.HasOne("Supermarket.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Supermarket.Models.WarehouseSection_Product", b =>
                {
                    b.HasOne("Supermarket.Models.Product", "Product")
                        .WithMany("WarehouseSection")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Supermarket.Models.WarehouseSection", "WarehouseSection")
                        .WithMany("Products")
                        .HasForeignKey("WarehouseSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("WarehouseSection");
                });

            modelBuilder.Entity("Supermarket.Models.Employee", b =>
                {
                    b.Navigation("MealCard");
                });

            modelBuilder.Entity("Supermarket.Models.IssueType", b =>
                {
                    b.Navigation("IssueTypes");
                });

            modelBuilder.Entity("Supermarket.Models.MealCard", b =>
                {
                    b.Navigation("CardMovements");
                });

            modelBuilder.Entity("Supermarket.Models.Product", b =>
                {
                    b.Navigation("Shelf");

                    b.Navigation("WarehouseSection");
                });

            modelBuilder.Entity("Supermarket.Models.Shelf", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("Supermarket.Models.WarehouseSection", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
