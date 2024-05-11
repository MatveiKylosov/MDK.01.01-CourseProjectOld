using MDK._01._01_CourseProject.Common.Database;
using MDK._01._01_CourseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MDK._01._01_CourseProject.Context
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomerContext()
        {
            Database.EnsureCreated();
            Customers.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
