using System;
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
        }

        public DbSet<Supermarket.Models.Folga> Folga { get; set; } = default!;

        public DbSet<Supermarket.Models.Funcoes> Funcoes { get; set; } = default!;

        public DbSet<EmployeeEvaluation> AvaliacaoFuncionarios { get; set; } = default!;
    }
}
