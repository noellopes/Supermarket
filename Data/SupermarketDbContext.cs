﻿using Microsoft.EntityFrameworkCore;
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

            modelBuilder.Entity<MealCard>().HasKey(MC => MC.MealCardId);
            //Relação entre Schedule e Departments
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Department)
                .WithMany()
                .HasForeignKey(s => s.DeptID);
            modelBuilder.Entity<Ticket>()
                .HasOne(s => s.Departments)
                .WithMany()
                .HasForeignKey(s => s.IDDepartments);

            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<Employee>().Property(e => e.EmployeeId).UseIdentityColumn();

            // Deletar em ExpiredProducts se uma Purchase for deletada
            modelBuilder.Entity<ExpiredProducts>()
                .HasOne(e => e.Purchase)
                .WithMany()
                .HasForeignKey(e => e.PurchaseId)
                .OnDelete(DeleteBehavior.Cascade);

            //Restringir delete de employee que esteja vinculado a uma Purchase
            modelBuilder.Entity<Employee>()
                .HasMany(p => p.Purchases)
                .WithOne(p => p.Employee)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            //Restringir delete de product que esteja vinculado a uma Purchase
            modelBuilder.Entity<Product>()
                .HasMany(e => e.Purchases)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            //Restringir delete de product que esteja vinculado a uma Purchase
            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Purchases)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IssueType>()
                .HasMany(it => it.Issue)
                .WithOne(i => i.IssueType)
                .HasForeignKey(i => i.IssueTypeId)
                .OnDelete(DeleteBehavior.Restrict);                

            //ModelBuilder das funções
            modelBuilder.Entity<Employee>()
                .HasOne(f => f.Funcao)
                .WithMany(e => e.Employees)
                .HasForeignKey(k =>k.Funcao_FK);

            modelBuilder.Entity<FuncaoGrupoProjeto>()
                .HasOne(fg => fg.funcao)
                .WithMany(gp => gp.GrupoProjetos)
                .HasForeignKey(fg => fg.FuncaoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FuncaoGrupoProjeto>()
                .HasOne(gp => gp.GrupoProjeto)
                .WithMany(f => f.Funcoes)
                .HasForeignKey(gp => gp.ProjetoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
 
        public DbSet<Folga> Folga { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Supermarket.Models.SubsidyCalculation> SubsidyCalculation { get; set; } = default!;

        public DbSet<Employee> Funcionarios { get; set; } = default!;

        public DbSet<Employee> Employee { get; set; } = default!;

      
        public DbSet<Supermarket.Models.IssueType> IssueType { get; set; } = default!;

        public DbSet<Supermarket.Models.Issues> Issues { get; set; } = default!;

        public DbSet<Supermarket.Models.ProductExpiration> ProductExpiration { get; set; } = default!;
        public DbSet<Supermarket.Models.ProductExpiration> ExpiredProducts { get; set; } = default!;


        public DbSet<Supermarket.Models.Funcao> Funcao { get; set; } = default!;

        public DbSet<EmployeeEvaluation> EmployeeEvaluation { get; set; } = default!;

        public DbSet<FormationEmployee>FormationEmployees { get; set; } = default!;

        public DbSet<Supermarket.Models.ProductDiscount> ProductDiscount { get; set; } = default!;

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

        public DbSet<Supermarket.Models.Reserve> Reserve { get; set; } = default!;

        public DbSet<Schedule> Schedule { get; set; }      

        public DbSet<CategoryDiscounts> CategoryDiscounts { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Supermarket.Models.ClientCard> ClientCard { get; set; } = default!;

        public DbSet<Supermarket.Models.Client> Client { get; set; } = default!;


        public DbSet<Supermarket.Models.ReserveDepartment1> ReserveDepartment { get; set; } = default!;

        public DbSet<Supermarket.Models.Reserve> Reserves { get; set; } = default!;


        public DbSet<MealCard> MealCard { get; set; } = default!;

        public DbSet<CardMovement> CardMovement { get; set; } = default!;
        public DbSet<Supermarket.Models.Ponto> Ponto { get; set; } = default!;

        public DbSet<Supermarket.Models.EmployeeSchedule> EmployeeSchedule { get; set; } = default!;

        public DbSet<SubsidySetup> SubsidySetup { get; set; } = default!;


        public DbSet<Supermarket.Models.Formation> Formation { get; set; } = default!;


        public DbSet<HierarquiasModel> Hierarquias { get; set; } = default!;

        public DbSet<Supermarket.Models.Supplier> Suppliers { get; set; } = default!;

        public DbSet<Supermarket.Models.MealCard> MealCards { get; set; } = default!;
        
        public DbSet<Supermarket.Models.Purchase> Purchase { get; set; } = default!;

        public object Pontos { get; internal set; }


        ///public object Pontos { get; internal set; }
        public DbSet<TakeAwayCategory> TakeAwayCategory { get; set; } = default!;
        public DbSet<TakeAwayProduct> TakeAwayProduct { get; set; } = default!;
        public DbSet<User_Order> User_Order { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<Supermarket.Models.Alert> Alert { get; set; } = default!;

        public DbSet<Orders> Orders { get; set; } = default!;
        public DbSet<Supermarket.Models.GrupoProjeto> GrupoProjeto { get; set; } = default!;
        public DbSet<Supermarket.Models.FuncaoGrupoProjeto> FuncaoGrupoProjeto { get; set; } = default!;

    }
}
