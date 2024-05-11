using MDK._01._01_CourseProject.Common.Database;
using MDK._01._01_CourseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MDK._01._01_CourseProject.Context
{
    public class CarSaleContext : DbContext
    {
        public DbSet<CarSale> CarSales { get; set; }
        public CarSaleContext()
        {
            Database.EnsureCreated();
            CarSales.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
