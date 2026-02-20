using Microsoft.EntityFrameworkCore;
using OGE.Model;
using System.Collections.Generic;

namespace OGE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Schoolchildren> Schoolchildrens { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Auditorium> Auditorium { get; set; }
    }
}
