using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.Context;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows;
using Schema = System.ComponentModel.DataAnnotations.Schema;
namespace MDK._01._01_CourseProject.Models
{
    public class CarSale : NotMappedNotification
    {
        [Key]
        public int SaleID { get; set; }

        private DateTime? saleDate;
        private int? employeeID;
        private int? carID;
        private int? customerID;

        public DateTime? SaleDate
        {
            get { return saleDate; }
            set
            {
                Match match = Regex.Match(value.ToString(), @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9] (0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[012])\.\d{4}");
                if (match.Success)
                {
                    saleDate = value;
                    OnPropertyChanged("SaleDate");
                }
                else
                    MessageBox.Show($"Ошибка", "Укажите дату продажи в формате - \"ЧЧ:ММ дд.мм.гггг\".");
            }
        }
        public int? EmployeeID
        {
            get { return employeeID; }
            set
            {
                employeeID = value;
                OnPropertyChanged("EmployeeID");
            }
        }

        public int? CarID
        {
            get { return carID; }
            set
            {
                carID = value;
                OnPropertyChanged("CarID");
            }
        }
        public int? CustomerID
        {
            get { return customerID; }
            set
            {
                customerID = value;
                OnPropertyChanged("CustomerID");
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
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCarSale.CarSaleContext.SaveChanges();
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
                    if (MessageBox.Show("Вы уверены что хотите удалить запись продажи?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCarSale.CarSale.Remove(this);
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCarSale.CarSaleContext.Remove(this);
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCarSale.CarSaleContext.SaveChanges();
                    }
                }
                );
            }
        }

        [Schema.NotMapped]
        public ObservableCollection<Car> Car { get { return new ObservableCollection<Car>(new CarContext().Cars); } }
        [Schema.NotMapped]
        public ObservableCollection<Employee> Employee { get { return new ObservableCollection<Employee>(new EmployeeContext().Employees); } }
        [Schema.NotMapped]
        public ObservableCollection<Customer> Customer { get { return new ObservableCollection<Customer>(new CustomerContext().Customers); } }

    }
}
