using ExamRegistrationSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamRegistrationSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Exam> Exams => Set<Exam>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasKey(c => c.CourseCode);
        modelBuilder.Entity<Student>().HasKey(s => s.StudentNumber);
        modelBuilder.Entity<Exam>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Exams)
            .HasForeignKey(e => e.CourseCode);

        modelBuilder.Entity<Exam>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Exams)
            .HasForeignKey(e => e.StudentNumber);
    }
}
