using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.Context;
using MDK._01._01_CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDK._01._01_CourseProject.ViewModels
{
    public class VMEmployee
    {
        public EmployeeContext EmployeeContext { get; set; }
        public ObservableCollection<Employee> Employee { get; set; }
        public VMEmployee()
        {
            Employee = new ObservableCollection<Employee>(EmployeeContext.Employees);
        }

        public RelayCommand AddEmployee
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Employee Employee = new Employee();

                    this.Employee.Add(Employee);
                    EmployeeContext.Employees.Add(Employee);
                    EmployeeContext.SaveChanges();
                }
                );
            }
        }
    }
}
