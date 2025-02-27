using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITI_project.Models
{
    public class Context:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Department> Departments { set; get; }
        public DbSet<Course> Courses { set; get; }
        public DbSet<Instructor> Instructores { set; get; }
        public DbSet<Trainee> Trainees { set; get; }
        public DbSet<CourseRes> CourseRes { set; get; }

        public Context():base()
        {

        }
        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=AHMED33;Initial Catalog=ITI_project_2;Integrated Security=True;Encrypt=False");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DeptID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Course)
                .WithMany(c => c.Instructors)
                .HasForeignKey(i => i.CourseID)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascading delete

            modelBuilder.Entity<Trainee>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Trainees)
                .HasForeignKey(t => t.DeptID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<CourseRes>()
                .HasOne(cr => cr.Course)
                .WithMany(c => c.CrsRes)
                .HasForeignKey(cr => cr.CourseID)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascading delete

            modelBuilder.Entity<CourseRes>()
                .HasOne(cr => cr.Trainee)
                .WithMany(t => t.CrsRes)
                .HasForeignKey(cr => cr.TraineeID)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascading delete
        }

    }
}
