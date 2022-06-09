using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext :DbContext
    {
       public DataContext(DbContextOptions options) : base(options) {}  

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Unit> Units { get; set; }
    }
}