using MDK._01._01_CourseProject.Common;
using Schema = System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Windows;
using System.Linq;

namespace MDK._01._01_CourseProject.Models
{
    public class Car : NotMappedNotification
    {
        private string carName;
        private int brandID;
        private int yearOfProduction;
        private string color;
        private string category;
        private decimal price;

        public int CarID { get; set; }
        public string CarName
        {
            get { return carName; }
            set
            {
                Match match = Regex.Match(value, "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    carName = value;
                    OnPropertyChanged("CarName");
                }
            }
        }
        public int BrandID
        {
            get { return brandID; }
            set
            {
                brandID = value;
                OnPropertyChanged("BrandID");
            }
        }
        public int YearOfProduction
        {
            get { return yearOfProduction; }
            set
            {
                yearOfProduction = value;
                OnPropertyChanged("YearOfProduction");
            }
        }
        public string Color
        {
            get { return color; }
            set
            {
                Match match = Regex.Match(value, "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    color = value;
                    OnPropertyChanged("Color");
                }
            }
        }
        public string Category
        {
            get { return category; }
            set
            {
                Match match = Regex.Match(value, "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    category = value;
                    OnPropertyChanged("Category");
                }
            }
        }
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
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
                    if (MessageBox.Show("Вы уверены что хотите удалить запись машины?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var VMCarSale = (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCarSale;
                        var VMCar = (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCar;

                        foreach (var carSale in VMCarSale.CarSale.Where(x => x.CarID == this.CarID).ToList())
                        {
                            VMCarSale.CarSale.Remove(carSale);
                            VMCarSale.CarSaleContext.Remove(carSale);
                        }
                        VMCarSale.CarSaleContext.SaveChanges();

                        VMCar.Car.Remove(this);
                        VMCar.CarContext.Remove(this);
                        VMCar.CarContext.SaveChanges();
                    }
                });
            }
        }

    }
}
