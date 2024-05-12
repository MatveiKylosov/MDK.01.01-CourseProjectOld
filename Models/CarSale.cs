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
        private int carID;
        private int customerID;

        public int SaleID { get; set; }
        public DateTime SaleDate
        {
            get { return saleDate; }
            set
            {
                saleDate = value;
                OnPropertyChanged("SaleDate");
            }
        }
        public int EmployeeID
        {
            get { return employeeID; }
            set
            {
                employeeID = value;
                OnPropertyChanged("EmployeeID");
            }
        }

        public int CarID
        {
            get { return carID; }
            set
            {
                carID = value;
                OnPropertyChanged("CarID");
            }
        }
        public int CustomerID
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
    }
}
