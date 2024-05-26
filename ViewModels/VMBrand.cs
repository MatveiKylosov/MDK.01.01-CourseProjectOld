using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.Context;
using MDK._01._01_CourseProject.Models;
using MDK._01._01_CourseProject.View.Brands;
using Microsoft.Win32;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace MDK._01._01_CourseProject.ViewModels
{
    public class VMBrand
    {
        public Filter filter = new Filter();
        public BrandContext BrandContext = new BrandContext();
        public ObservableCollection<Brand> Brand {  get; set; }
        public VMBrand()
        {
            Brand = new ObservableCollection<Brand>(BrandContext.Brands);
        }
        public RelayCommand AddBrand { get
            {
                return new RelayCommand(obj =>
                    { 
                        Brand brand = new Brand();

                        this.Brand.Add(brand);
                        BrandContext.Brands.Add(brand);
                        BrandContext.SaveChanges();
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
                        ExportBrandData(Brand.ToList(), saveFileDialog.FileName);
                    
                }
                );
            }
        }

        public RelayCommand Filter
        {
            get
            {
                return new RelayCommand(obj =>
                    {
                        filter.SetupData(BrandContext.GetAllCountries(), BrandContext.GetAllManufacturers(), BrandContext.GetAllAddresses());
                        if (filter.ShowDialog().Value)
                        {
                            var itemsToRemove = new List<Brand>();

                            if (filter.country != "Не выбран.")
                                itemsToRemove.AddRange(Brand.Where(x => x.Country != filter.country));

                            if (filter.manufacture != "Не выбран.")
                                itemsToRemove.AddRange(Brand.Where(x => x.BrandName != filter.manufacture));

                            if (filter.address != "Не выбран.")
                                itemsToRemove.AddRange(Brand.Where(x => x.Address != filter.address));

                            foreach (var item in itemsToRemove)
                                Brand.Remove(item);
                        }
                        else Brand = new ObservableCollection<Brand>(BrandContext.Brands);
                    }
                );
            }
        }

        public void ExportBrandData(List<Brand> brands, string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Brands");

                // Заголовки столбцов
                worksheet.Cells[1, 1].Value = "BrandID";
                worksheet.Cells[1, 2].Value = "BrandName";
                worksheet.Cells[1, 3].Value = "Country";
                worksheet.Cells[1, 4].Value = "Manufacturer";
                worksheet.Cells[1, 5].Value = "Address";

                // Заполнение данными
                for (int i = 0; i < brands.Count; i++)
                {
                    var brand = brands[i];
                    worksheet.Cells[i + 2, 1].Value = brand.BrandID;
                    worksheet.Cells[i + 2, 2].Value = brand.BrandName;
                    worksheet.Cells[i + 2, 3].Value = brand.Country;
                    worksheet.Cells[i + 2, 4].Value = brand.Manufacturer;
                    worksheet.Cells[i + 2, 5].Value = brand.Address;
                }

                // Сохранение в файл
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
    }
}
