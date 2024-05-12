using MDK._01._01_CourseProject.Common;
using System;
using System.Windows;
using Schema = System.ComponentModel.DataAnnotations.Schema;
namespace MDK._01._01_CourseProject.Models
{
    public class CarSale : NotMappedNotification
    {
        private DateTime saleDate;
        private int employeeID;
        private Employee employee;
        private int carID;
        private Car car;
        private int customerID;
        private Customer customer;

        public int SaleID { get; set; }
        public DateTime SaleDate
        {
            get { return saleDate; }
            set
            {
                saleDate = value;
                OnPropertyChanged("");
            }
        }
        public int EmployeeID
        {
            get { return employeeID; }
            set
            {
                employeeID = value;
                OnPropertyChanged("");
            }
        }
        public Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged("");
            }
        }
        public int CarID
        {
            get { return carID; }
            set
            {
                carID = value;
                OnPropertyChanged("");
            }
        }
        public Car Car
        {
            get { return car; }
            set
            {
                car = value;
                OnPropertyChanged("");
            }
        }
        public int CustomerID
        {
            get { return customerID; }
            set
            {
                customerID = value;
                OnPropertyChanged("");
            }
        }
        public Customer Customer
        {
            get { return customer; }
            set
            {
                customer = value;
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
    }
}
