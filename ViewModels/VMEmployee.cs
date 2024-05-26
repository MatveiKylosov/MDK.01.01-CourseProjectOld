using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.Context;
using MDK._01._01_CourseProject.Models;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDK._01._01_CourseProject.ViewModels
{
    public class VMEmployee
    {
        public EmployeeContext EmployeeContext = new EmployeeContext();
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

        public RelayCommand Export
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
                    saveFileDialog.FilterIndex = 0;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.Title = "Export Excel File To";

                    if (saveFileDialog.ShowDialog().Value)
                        ExportEmployeeData(Employee.ToList(), saveFileDialog.FileName);

                }
                );
            }
        }

        public void ExportEmployeeData(List<Employee> employees, string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                // Заголовки столбцов
                worksheet.Cells[1, 1].Value = "EmployeeID";
                worksheet.Cells[1, 2].Value = "FullName";
                worksheet.Cells[1, 3].Value = "WorkExperience";
                worksheet.Cells[1, 4].Value = "Salary";
                worksheet.Cells[1, 5].Value = "ContactDetails";

                // Заполнение данными
                for (int i = 0; i < employees.Count; i++)
                {
                    var employee = employees[i];
                    worksheet.Cells[i + 2, 1].Value = employee.EmployeeID;
                    worksheet.Cells[i + 2, 2].Value = employee.FullName;
                    worksheet.Cells[i + 2, 3].Value = employee.WorkExperience;
                    worksheet.Cells[i + 2, 4].Value = employee.Salary;
                    worksheet.Cells[i + 2, 5].Value = employee.ContactDetails;
                }

                // Сохранение в файл
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
    }
}
