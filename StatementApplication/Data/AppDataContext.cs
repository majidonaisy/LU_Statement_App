using Microsoft.EntityFrameworkCore;
using StatementApplication.Models;

namespace StatementApplication.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options)
        : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Statement> Statement { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the foreign key relationship
            modelBuilder.Entity<Statement>()
                .HasOne(s => s.Student)
                .WithMany(st => st.Statements)
                .HasForeignKey(s => s.StudentId);
            modelBuilder.Entity<Application>()
            .HasOne(a => a.Student)
            .WithMany(s => s.Applications)
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.NoAction);// Set the desired delete behavior

            base.OnModelCreating(modelBuilder);
        }

    }
}
