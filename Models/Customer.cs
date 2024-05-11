using System;

namespace MDK._01._01_CourseProject.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string PassportData { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string ContactDetails { get; set; }
    }
}
