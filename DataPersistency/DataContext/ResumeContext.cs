using DataPersistency.Models;
using Microsoft.EntityFrameworkCore;

namespace DataPersistency.DataContext
{
    public class ResumeContext : DbContext
    {
        public ResumeContext(DbContextOptions<ResumeContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Degree> Degrees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>().HasKey(x => x.Id);
            modelBuilder.Entity<Degree>().HasKey(x => x.Id);
        }
    }
}
