using Microsoft.EntityFrameworkCore;
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
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<Employee>().Property(e => e.EmployeeId).UseIdentityColumn();

        }

        public DbSet<Folga> Folga { get; set; } = default!;
        public DbSet<Supermarket.Models.SubsidyCalculation> SubsidyCalculation { get; set; } = default!;

        public DbSet<Employee> Funcionarios { get; set; } = default!;

        public DbSet<Employee> Employee { get; set; } = default!;

        public DbSet<Supermarket.Models.IssueType> IssueType { get; set; } = default!;

        public DbSet<Supermarket.Models.Issues> Issues { get; set; } = default!;

        public DbSet<Supermarket.Models.ProductExpiration> ProductExpiration { get; set; } = default!;

        public DbSet<Supermarket.Models.Funcao> Funcao { get; set; } = default!;

        public DbSet<EmployeeEvaluation> EmployeeEvaluation { get; set; } = default!;

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

        public DbSet<Supermarket.Models.ClientCard> ClientCard { get; set; } = default!;

        public DbSet<Supermarket.Models.Client> Client { get; set; } = default!;

        public DbSet<Supermarket.Models.CategoryDiscount> CategoryDiscounts { get; set; } = default!;

        //public DbSet<Supermarket.Models.ReserveDepartment> ReserveDepartment { get; set; } = default!;

        public DbSet<Supermarket.Models.Reserve> Reserve { get; set; } = default!;

        public DbSet<MealCard> MealCard { get; set; } = default!;

        public DbSet<CardMovement> CardMovement { get; set; } = default!;
        public DbSet<Supermarket.Models.Ponto> Ponto { get; set; } = default!;

        public DbSet<Supermarket.Models.EmployeeSchedule> EmployeeSchedule { get; set; } = default!;

        public DbSet<SubsidySetup> SubsidySetup { get; set; } = default!;
<<<<<<< HEAD

        public DbSet<HierarquiasModel> Hierarquias { get; set; } = default!;

        public DbSet<Supermarket.Models.Supplier> Suppliers { get; set; } = default!;

        public DbSet<Supermarket.Models.MealCard> MealCards { get; set; } = default!;
        
        public DbSet<Supermarket.Models.ExpiredProducts> ExpiredProducts { get; set; } = default!;
        
        public DbSet<Supermarket.Models.Purchase> Purchase { get; set; } = default!;

=======
        public object Pontos { get; internal set; }
>>>>>>> Grupo2
    }
}
