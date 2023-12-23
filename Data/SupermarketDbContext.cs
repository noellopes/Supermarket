using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DbSet<Supermarket.Models.Folga> Folga { get; set; } = default!;

        public DbSet<Supermarket.Models.PurchaseSupplier> PurchaseSupplier { get; set; } = default!;

        public DbSet<Supermarket.Models.Addresses> Addresses { get; set; } = default!;

        public DbSet<Supermarket.Models.Supplier> Supplier { get; set; } = default!;

        public DbSet<Supermarket.Models.SupplierAvailability> SupplierAvailability { get; set; } = default!;

    }
}
