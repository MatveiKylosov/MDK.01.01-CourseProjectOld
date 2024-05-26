using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.Context;
using MDK._01._01_CourseProject.Models;
using System.Collections.ObjectModel;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using System.Linq;

namespace MDK._01._01_CourseProject.ViewModels
{
    public class VMCar
    {
        public CarContext CarContext = new CarContext();
        public ObservableCollection<Car> Car { get; set; }
        public VMCar()
        {
            Car = new ObservableCollection<Car>(CarContext.Cars);
        }

        public RelayCommand AddCar
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Car car = new Car();

                    this.Car.Add(car);
                    CarContext.Cars.Add(car);
/*                    CarContext.SaveChanges();*/
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
                        ExportCarData(Car.ToList(), saveFileDialog.FileName);

                }
                );
            }
        }

        public void ExportCarData(List<Car> cars, string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Cars");

                // Заголовки столбцов
                worksheet.Cells[1, 1].Value = "CarID";
                worksheet.Cells[1, 2].Value = "CarName";
                worksheet.Cells[1, 3].Value = "BrandID";
                worksheet.Cells[1, 4].Value = "YearOfProduction";
                worksheet.Cells[1, 5].Value = "Color";
                worksheet.Cells[1, 6].Value = "Category";
                worksheet.Cells[1, 7].Value = "Price";

                // Заполнение данными
                for (int i = 0; i < cars.Count; i++)
                {
                    var car = cars[i];
                    worksheet.Cells[i + 2, 1].Value = car.CarID;
                    worksheet.Cells[i + 2, 2].Value = car.CarName;
                    worksheet.Cells[i + 2, 3].Value = car.BrandID;
                    worksheet.Cells[i + 2, 4].Value = car.YearOfProduction;
                    worksheet.Cells[i + 2, 5].Value = car.Color;
                    worksheet.Cells[i + 2, 6].Value = car.Category;
                    worksheet.Cells[i + 2, 7].Value = car.Price;
                }

                // Сохранение в файл
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
    }
}
