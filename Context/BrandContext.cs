using MDK._01._01_CourseProject.Common.Database;
using MDK._01._01_CourseProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public List<string> GetAllBrandName()
        {
            return Brands
                .Select(b => b.BrandName)
                .Distinct()
                .ToList();
        }

        public List<string> GetAllCountries()
        {
            return Brands
                .Select(b => b.Country)
                .Distinct()
                .ToList();
        }

        public List<string> GetAllManufacturers()
        {
            return Brands
                .Select(b => b.Manufacturer)
                .Distinct()
                .ToList();
        }

        public List<string> GetAllAddresses()
        {
            return Brands
                .Select(b => b.Address)
                .Distinct()
                .ToList();
        }
    }
}
