using MDK._01._01_CourseProject.Common.Database;
using MDK._01._01_CourseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MDK._01._01_CourseProject.Context
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public EmployeeContext()
        {
            Database.EnsureCreated();
            Employees.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
