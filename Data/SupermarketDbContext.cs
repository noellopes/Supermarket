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

        public DbSet<Supermarket.Models.Funcoes> Funcoes { get; set; } = default!;

        public DbSet<EmployeeEvaluation> AvaliacaoFuncionarios { get; set; } = default!;

        public DbSet<Employee> Funcionarios { get; set; } = default!;

        public DbSet<Supermarket.Models.Schedule> Schedule { get; set; } = default!;

        public DbSet<Supermarket.Models.Tickets> Tickets { get; set; } = default!;

        public DbSet<Supermarket.Models.Departments> Departments { get; set; } = default!;
    }
}
