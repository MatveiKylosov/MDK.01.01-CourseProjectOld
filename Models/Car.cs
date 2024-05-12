using MDK._01._01_CourseProject.Common;
using Schema = System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Windows;

namespace MDK._01._01_CourseProject.Models
{
    public class Car : NotMappedNotification
    {
        private string carName;
        private int brandID;
        private Brand brand;
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
                    OnPropertyChanged("");
                }
            }
        }
        public int BrandID
        {
            get { return brandID; }
            set
            {
                brandID = value;
                OnPropertyChanged("");
            }
        }
        public Brand Brand
        {
            get { return brand; }
            set
            {
                brand = value;
                OnPropertyChanged("");
            }
        }
        public int YearOfProduction
        {
            get { return yearOfProduction; }
            set
            {
                yearOfProduction = value;
                OnPropertyChanged("");
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
                    OnPropertyChanged("");
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
                    OnPropertyChanged("");
                }
            }
        }
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("");
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
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCar.Car.Remove(this);
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCar.CarContext.Remove(this);
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCar.CarContext.SaveChanges();
                    }
                }
                );
            }
        }
    }
}
