using MDK._01._01_CourseProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDK._01._01_CourseProject.ViewModels
{
    public class VMPages
    {
        public VMBrand      VMBrand;
        public VMCar        VMCar;
        public VMCarSale    VMCarSale;
        public VMCustomer   VMCustomer;
        public VMEmployee   VMEmployee;

        VMPages()
        {
            MainWindow.Instance.Frame.Navigate(new View.Cars.Main(VMCar));
        }


        public RelayCommand OpenBrand
        {
            get
            {
                return new RelayCommand(obj => {  MainWindow.Instance.Frame.Navigate(new View.Brands.Main(VMBrand)); });
            }
        }

        public RelayCommand OpenCar
        {
            get
            {
                return new RelayCommand(obj => {  MainWindow.Instance.Frame.Navigate(new View.Cars.Main(VMCar)); });
            }
        }

        public RelayCommand OpenCarSale
        {
            get
            {
                return new RelayCommand(obj => {  MainWindow.Instance.Frame.Navigate(new View.CarSales.Main(VMCarSale)); });
            }
        }

        public RelayCommand OpenCustomer
        {
            get
            {
                return new RelayCommand(obj => {  MainWindow.Instance.Frame.Navigate(new View.Customers.Main(VMCustomer)); });
            }
        }

        public RelayCommand OpenEmployee
        {
            get
            {
                return new RelayCommand(obj => {  MainWindow.Instance.Frame.Navigate(new View.Employees.Main(VMEmployee)); });
            }
        }
    }
}
