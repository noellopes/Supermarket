﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Supermarket.Models;

namespace Supermarket.Data
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext (DbContextOptions<SupermarketDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeEvaluation>().HasKey(EE => EE.EmployeeEvaluationId);
            modelBuilder.Entity<Meal_Card>().HasKey(MC => MC.Card_Id);
            modelBuilder.Entity<Departments>().HasKey(MC => MC.Id_Departments);
            modelBuilder.Entity<Tickets>().HasKey(MC => MC.TicketId);
        }

        public DbSet<Supermarket.Models.Folga> Folga { get; set; } = default!;


        public DbSet<Supermarket.Models.IssueType> IssueType { get; set; } = default!;

        public DbSet<Supermarket.Models.Issues> Issues { get; set; } = default!;

        public DbSet<Supermarket.Models.ProductExpiration> ProductExpiration { get; set; } = default!;

        public DbSet<Supermarket.Models.Funcoes> Funcoes { get; set; } = default!;

        public DbSet<EmployeeEvaluation> AvaliacaoFuncionarios { get; set; } = default!;

        public DbSet<Employee> Funcionarios { get; set; } = default!;

  

        public DbSet<ProductDiscount> ProductDiscount { get; internal set; }

        public DbSet<Supermarket.Models.Product> Product { get; set; } = default!;

        public DbSet<Supermarket.Models.Hallway> Hallway { get; set; } = default!;

        public DbSet<Supermarket.Models.Shelf> Shelf { get; set; } = default!;

        public DbSet<Supermarket.Models.Shelft_ProductExhibition> Shelft_ProductExhibition { get; set; } = default!;

        public DbSet<Supermarket.Models.Brand> Brand { get; set; } = default!;

        public DbSet<Supermarket.Models.Category> Category { get; set; } = default!;

        public DbSet<Supermarket.Models.ReduceProduct> ReduceProduct { get; set; } = default!;

        public DbSet<Supermarket.Models.Warehouse> Warehouse { get; set; } = default!;

        public DbSet<Supermarket.Models.WarehouseSection> WarehouseSection { get; set; } = default!;

        public DbSet<Supermarket.Models.WarehouseSection_Product> WarehouseSection_Product { get; set; } = default!;

        public DbSet<Supermarket.Models.Store> Store { get; set; } = default!;

        public DbSet<Supermarket.Models.Schedule> Schedule { get; set; } = default!;

        public DbSet<Supermarket.Models.Tickets> Tickets { get; set; } = default!;

        public DbSet<Supermarket.Models.Departments> Departments { get; set; } = default!;


    }
}
