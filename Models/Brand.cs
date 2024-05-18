using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Animation;
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
                if (value.Length > 0 && value.Length <= 255)
                {
                    brandName = value;
                    OnPropertyChanged("BrandName");
                }
                else
                    MessageBox.Show($"Ошибка", "Название марки машины должно быть больше 0 и меньше 255 символов");
            }
        }
        public string Country
        { 
            get{ return country; } 
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }
        public string Manufacturer 
        { 
            get{ return manufacturer; } 
            set
            {
                if (value.Length > 0 && value.Length <= 255)
                {
                    manufacturer = value;
                    OnPropertyChanged("Manufacturer");
                }
                else
                    MessageBox.Show($"Ошибка", "Название завода должно быть больше 0 и меньше 255 символов");
            }
        }
        public string Address 
        { 
            get{ return address; } 
            set
            {
                if (value.Length > 0 && value.Length <= 255)
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
                else
                    MessageBox.Show($"Ошибка", "Адрес должен быть больше 0 и меньше 255 символов");
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
                    var result = MessageBox.Show("Удалить вместе с этой записью другие?", "Предупреждение", MessageBoxButton.YesNoCancel);

                    if (result == MessageBoxResult.Yes || result == MessageBoxResult.No)
                    {
                        var VMPages = MainWindow.Instance.DataContext as ViewModels.VMPages;
                        var VMBrand = VMPages.VMBrand;
                        var VMCar = VMPages.VMCar;
                        var VMCarSale = VMPages.VMCarSale;

                        if (result == MessageBoxResult.Yes)
                        {
                            var carSalesToRemove = VMCarSale.CarSale.Where(x => VMCar.CarContext.Cars.Any(c => c.CarID == x.CarID && c.BrandID == this.BrandID)).ToList();
                            foreach (var carSale in carSalesToRemove)
                            {
                                VMCarSale.CarSale.Remove(carSale);
                                VMCarSale.CarSaleContext.Remove(carSale);
                            }

                            var carsToRemove = VMCar.CarContext.Cars.Where(x => x.BrandID == this.BrandID).ToList();
                            foreach (var car in carsToRemove)
                            {
                                VMCar.Car.Remove(car);
                                VMCar.CarContext.Remove(car);
                            }

                            VMCarSale.CarSaleContext.SaveChanges();
                        }
                        else
                        {
                            var carsToUpdate = VMCar.CarContext.Cars.Where(x => x.BrandID == this.BrandID).ToList();
                            var carsToUpdateContext = VMCar.CarContext.Cars.Where(x => x.BrandID == this.BrandID).ToList();
                            foreach (var car in carsToUpdate)
                            {
                                car.BrandID = null;
                                carsToUpdateContext.First(x => x.CarID == car.CarID).BrandID = null;
                            }
                        }

                        VMCar.CarContext.SaveChanges();

                        VMBrand.Brand.Remove(this);
                        VMBrand.BrandContext.Remove(this);
                        VMBrand.BrandContext.SaveChanges();
                    }
                });
            }
        }
    }
}
