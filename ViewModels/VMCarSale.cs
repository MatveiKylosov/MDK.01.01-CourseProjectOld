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
    public class VMCarSale
    {
        public CarSaleContext CarSaleContext = new CarSaleContext();
        public ObservableCollection<CarSale> CarSale { get; set; }
        public VMCarSale() 
        { 
            CarSale = new ObservableCollection<CarSale>(CarSaleContext.CarSales);
        }

        public RelayCommand AddCarSale
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    CarSale carSale = new CarSale();

                    this.CarSale.Add(carSale);
                    CarSaleContext.CarSales.Add(carSale);
                    CarSaleContext.SaveChanges();
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
                        ExportCarSaleData(CarSale.ToList(), saveFileDialog.FileName);

                }
                );
            }
        }
        public void ExportCarSaleData(List<CarSale> carSales, string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("CarSales");

                // Заголовки столбцов
                worksheet.Cells[1, 1].Value = "SaleID";
                worksheet.Cells[1, 2].Value = "SaleDate";
                worksheet.Cells[1, 3].Value = "EmployeeID";
                worksheet.Cells[1, 4].Value = "CarID";
                worksheet.Cells[1, 5].Value = "CustomerID";

                // Заполнение данными
                for (int i = 0; i < carSales.Count; i++)
                {
                    var carSale = carSales[i];
                    worksheet.Cells[i + 2, 1].Value = carSale.SaleID;
                    worksheet.Cells[i + 2, 2].Value = carSale.SaleDate;
                    worksheet.Cells[i + 2, 3].Value = carSale.EmployeeID;
                    worksheet.Cells[i + 2, 4].Value = carSale.CarID;
                    worksheet.Cells[i + 2, 5].Value = carSale.CustomerID;
                }

                // Сохранение в файл
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
    }
}
