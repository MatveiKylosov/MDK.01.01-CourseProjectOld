using MDK._01._01_CourseProject.Common;
using Schema = System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using MDK._01._01_CourseProject.Context;

namespace MDK._01._01_CourseProject.Models
{
    public class Car : NotMappedNotification
    {
        private string carName;
        private int? brandID;
        private int? yearOfProduction;
        private string color;
        private string category;
        private decimal? price;

        public int CarID { get; set; }
        public string CarName
        {
            get { return carName; }
            set
            {
                if (value.Length > 0 && value.Length <= 255)
                {
                    carName = value;
                    OnPropertyChanged("CarName");
                }
                else
                {
                    MessageBox.Show($"Ошибка", "Название машины должно быть больше 0 и меньше 255 символов");
                }
            }
        }
        public int? BrandID
        {
            get { return brandID; }
            set
            {
                brandID = value;
                OnPropertyChanged("BrandID");
            }
        }
        public int? YearOfProduction
        {
            get { return yearOfProduction; }
            set
            {
                Match match = Regex.Match(value.ToString(), @"\b\d{4}\b");
                if (match.Success) {
                    yearOfProduction = value;
                    OnPropertyChanged("YearOfProduction");
                }
            }
        }
        public string Color
        {
            get { return color; }
            set
            {
                if (value.Length > 0 && value.Length <= 255)
                {
                    color = value;
                    OnPropertyChanged("Color");
                }
                else
                    MessageBox.Show($"Ошибка", "Название машины должно быть больше 0 и меньше 255 символов");
            }
        }
        public string Category
        {
            get { return category; }
            set
            {
                if (value.Length > 0 && value.Length <= 255)
                {
                    category = value;
                    OnPropertyChanged("Category");
                }
                else 
                    MessageBox.Show($"Ошибка", "Название машины должно быть больше 0 и меньше 255 символов");
            }
        }
        public decimal? Price
        {
            get { return price; }
            set
            {
                Match match = Regex.Match(value.ToString(), @"^\d*[\.,]?\d*$");

                if (match.Success)
                {
                    price = value;
                    OnPropertyChanged("Price");
                }
                else
                    MessageBox.Show($"Ошибка", "Цена должна быть положительным числом");
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
                            (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCar.CarContext.SaveChanges();
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
                        var VMCar = VMPages.VMCar;
                        var VMCarSale = VMPages.VMCarSale;

                        var CarSaleList = VMCarSale.CarSale.Where(x => x.CarID == this.CarID).ToList();

                        if (result == MessageBoxResult.Yes)
                        {
                           foreach (var carSale in CarSaleList)
                            {
                                VMCarSale.CarSale.Remove(carSale);
                                VMCarSale.CarSaleContext.Remove(carSale);
                            }
                        }
                        else
                        {
                            var carSalesContextToUpdate = VMCarSale.CarSaleContext.CarSales.Where(x => x.CarID == this.CarID).ToList();

                            foreach (var carSale in CarSaleList)
                            {
                                carSale.CarID = null;
                                carSalesContextToUpdate.First(x => x.CarID ==  carSale.CarID).CarID = null;
                            }
                        }

                        VMCar.Car.Remove(this);
                        VMCar.CarContext.Remove(this);

                        VMCarSale.CarSaleContext.SaveChanges();
                        VMCar.CarContext.SaveChanges();
                    }
                });
            }
        }

        [Schema.NotMapped]
        public ObservableCollection<Brand> Brand { get { return new ObservableCollection<Brand>(new BrandContext().Brands); } }
    }
}
