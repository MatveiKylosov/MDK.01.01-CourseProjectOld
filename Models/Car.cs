using System;

namespace MDK._01._01_CourseProject.Models
{
    public class Car
    {
        public int CarID { get; set; }
        public string CarName { get; set; }
        public int BrandID { get; set; }
        public Brand Brand { get; set; }
        public int YearOfProduction { get; set; }
        public string Color { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
