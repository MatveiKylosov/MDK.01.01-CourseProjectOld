using MDK._01._01_CourseProject.Common.Database;
using MDK._01._01_CourseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MDK._01._01_CourseProject.Context
{
    public class CarContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public CarContext()
        {
            Database.EnsureCreated();
            Cars.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
