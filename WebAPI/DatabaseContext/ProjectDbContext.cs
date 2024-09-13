using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.DatabaseContext
{
    public class ProjectDbContext:DbContext
    {
        public ProjectDbContext(DbContextOptions  options):base(options) 
        {
            

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=kdac;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<User> users { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Course> courses { get; set; }
      
        public DbSet<UserRole> roles { get; set; }
        public DbSet<CourseSubject> courseSubjects { get; set; }
        public DbSet<LogSheet> LogSheets { get; set; }
        public DbSet<LabPlan> labplans { get; set; }

    }
}
