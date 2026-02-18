using Microsoft.EntityFrameworkCore;
using OGE.Model;
using System.Collections.Generic;

namespace OGE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Schoolchildren> Schoolchildrens { get; set; }
        public DbSet<Subject1> Subject1 { get; set; }
        public DbSet<Subject2> Subject2 { get; set; }
    }
}
