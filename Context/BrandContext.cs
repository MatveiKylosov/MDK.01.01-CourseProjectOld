using MDK._01._01_CourseProject.Common.Database;
using MDK._01._01_CourseProject.Models;
using MDK._01._01_CourseProject.View.Brands.UserControls;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MDK._01._01_CourseProject.Context
{
    public class BrandContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public BrandContext()
        {
            Database.EnsureCreated();
            Brands.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
