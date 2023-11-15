using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Supermarket.Models;

namespace Supermarket.Data
{
    public class GroupContext : DbContext
    {
        public GroupContext (DbContextOptions<GroupContext> options)
            : base(options)
        {
        }

        public DbSet<Supermarket.Models.Group> Group { get; set; } = default!;
    }
}
