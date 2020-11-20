using Microsoft.EntityFrameworkCore;
using Session11Stickies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session11Stickies.Data
{
    public class StickiesDbContext : DbContext
    {
        public StickiesDbContext(DbContextOptions<StickiesDbContext> options) : base(options)
        { }

        public DbSet<Sticky> Stickies { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
