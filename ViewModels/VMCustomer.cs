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
    public class VMCustomer
    {
        public CustomerContext CustomerContext = new CustomerContext();
        public ObservableCollection<Customer> Customer { get; set; }
        public VMCustomer()
        {
            Customer = new ObservableCollection<Customer>(CustomerContext.Customers);
        }

        public RelayCommand AddCustomer
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Customer Customer = new Customer();

                    this.Customer.Add(Customer);
                    CustomerContext.Customers.Add(Customer);
                    CustomerContext.SaveChanges();
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
                        ExportCustomerData(Customer.ToList(), saveFileDialog.FileName);

                }
                );
            }
        }

        public void ExportCustomerData(List<Customer> customers, string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Customers");

                // Заголовки столбцов
                worksheet.Cells[1, 1].Value = "FullName";
                worksheet.Cells[1, 2].Value = "PassportData";
                worksheet.Cells[1, 3].Value = "Address";
                worksheet.Cells[1, 4].Value = "BirthDate";
                worksheet.Cells[1, 5].Value = "Gender";
                worksheet.Cells[1, 6].Value = "ContactDetails";

                // Заполнение данными
                for (int i = 0; i < customers.Count; i++)
                {
                    var customer = customers[i];
                    worksheet.Cells[i + 2, 1].Value = customer.FullName;
                    worksheet.Cells[i + 2, 2].Value = customer.PassportData;
                    worksheet.Cells[i + 2, 3].Value = customer.Address;
                    worksheet.Cells[i + 2, 4].Value = customer.BirthDate;
                    worksheet.Cells[i + 2, 5].Value = customer.Gender.Value ? "Male" : "Female";
                    worksheet.Cells[i + 2, 6].Value = customer.ContactDetails;
                }

                // Сохранение в файл
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
    }
}
