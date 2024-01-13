using Microsoft.EntityFrameworkCore;

namespace Supermarket.Data
{
    public class GroupsDbContext : DbContext
    {
        public GroupsDbContext(DbContextOptions<GroupsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Supermarket.Models.Group> Group { get; set; } = default!;

    }
}
