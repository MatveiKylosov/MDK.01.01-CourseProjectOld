using System;

namespace MDK._01._01_CourseProject.Models
{
    public class CarSale
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public int CarID { get; set; }
        public Car Car { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}
