using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OGE.Model;

// Псевдонимы
using AuditoriumModel = OGE.Model.Auditorium;
using SubjectModel = OGE.Model.Subject;
using SchoolchildrenModel = OGE.Model.Schoolchildren;

namespace OGE.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<SchoolchildrenModel> Schoolchildren { get; set; }
        public DbSet<SubjectModel> Subject { get; set; }
        public DbSet<AuditoriumModel> Auditorium { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SchoolchildrenModel>().Ignore(e => e.Name);

            modelBuilder.Entity<SubjectModel>()
                .HasOne(s => s.Schoolchildren)
                .WithMany()
                .HasForeignKey(s => s.SchoolchildrenId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}