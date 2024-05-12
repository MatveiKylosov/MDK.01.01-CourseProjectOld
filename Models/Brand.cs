using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace MDK._01._01_CourseProject.Models
{
    public class Brand : NotMappedNotification
    {
        private string brandName;
        private string country;
        private string manufacturer;
        private string address;

        public int BrandID { get; set; }
        public string BrandName 
        { 
            get{ return brandName; } 
            set
            {
                Match match = Regex.Match(value, "");
                if(!match.Success)
                    MessageBox.Show($"");
                else
                {
                    brandName = value;
                    OnPropertyChanged("BrandName");
                }
            }
        }
        public string Country 
        { 
            get{ return country; } 
            set
            {
                Match match = Regex.Match(value, "");
                if(!match.Success)
                    MessageBox.Show($"");
                else
                {
                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }
        public string Manufacturer 
        { 
            get{ return manufacturer; } 
            set
            {
                Match match = Regex.Match(value, "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    manufacturer = value;
                    OnPropertyChanged("Manufacturer");
                }
            }
        }
        public string Address 
        { 
            get{ return address; } 
            set
            {
                Match match = Regex.Match(value, "");
                if(!match.Success)
                    MessageBox.Show($"");
                else
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
            }
        }


        [Schema.NotMapped]
        public override RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    IsEnable = !IsEnable;

                    if (!IsEnable)
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMBrand.BrandContext.SaveChanges();
                }
                );

            }
        }

        [Schema.NotMapped]
        public override RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (MessageBox.Show("Вы уверены что хотите удалить запись бренда?\nВ случае удаления данной записи буду удалены другие, где она содержится", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var VMBrand = (MainWindow.Instance.DataContext as ViewModels.VMPages).VMBrand;
                        var VMCar = (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCar;
                        var VMCarSale = (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCarSale;

                        foreach (var carSale in VMCarSale.CarSale.Where(x => VMCar.CarContext.Cars.Any(c => c.CarID == x.CarID && c.BrandID == this.BrandID)).ToList())
                        {
                            VMCarSale.CarSale.Remove(carSale);
                            VMCarSale.CarSaleContext.Remove(carSale);
                        }
                        VMCarSale.CarSaleContext.SaveChanges();

                        foreach (var car in VMCar.CarContext.Cars.Where(x => x.BrandID == this.BrandID).ToList())
                        {
                            VMCar.Car.Remove(car);
                            VMCar.CarContext.Remove(car);
                        }
                        VMCar.CarContext.SaveChanges();

                        VMBrand.Brand.Remove(this);
                        VMBrand.BrandContext.Remove(this);
                        VMBrand.BrandContext.SaveChanges();
                    }
                }
                );
            }
        }
    }
}
