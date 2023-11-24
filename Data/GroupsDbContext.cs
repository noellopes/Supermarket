using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Supermarket.Models;

namespace Supermarket.Data
{
    public class GroupsDbContext : DbContext
    {
        public GroupsDbContext (DbContextOptions<GroupsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Supermarket.Models.Group> Group { get; set; } = default!;

    }
}
