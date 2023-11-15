using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Supermarket.Models;

namespace Supermarket.Data {
    public class FolgaDbContext : IdentityDbContext {
        public FolgaDbContext(DbContextOptions<FolgaDbContext> options)
            : base(options) {
        }
        public DbSet<Supermarket.Models.Folga> Folga { get; set; } = default!;
    }
}