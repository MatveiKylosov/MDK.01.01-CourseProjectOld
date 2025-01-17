﻿using MDK._01._01_CourseProject.Common;

namespace MDK._01._01_CourseProject.ViewModels
{
    public class VMPages
    {
        public VMBrand VMBrand = new VMBrand();
        public VMCar VMCar = new VMCar();
        public VMCarSale VMCarSale = new VMCarSale();
        public VMCustomer VMCustomer = new VMCustomer();
        public VMEmployee VMEmployee = new VMEmployee();

        public VMPages()
        {
            MainWindow.Instance.Frame.Navigate(new View.Brands.Main(VMBrand));
        }


        public RelayCommand OpenBrand
        {
            get
            {
                return new RelayCommand(obj => { MainWindow.Instance.Frame.Navigate(new View.Brands.Main(VMBrand)); });
            }
        }

        public RelayCommand OpenCar
        {
            get
            {
                return new RelayCommand(obj => { MainWindow.Instance.Frame.Navigate(new View.Cars.Main(VMCar)); });
            }
        }

        public RelayCommand OpenCarSale
        {
            get
            {
                return new RelayCommand(obj => { MainWindow.Instance.Frame.Navigate(new View.CarSales.Main(VMCarSale)); });
            }
        }

        public RelayCommand OpenCustomer
        {
            get
            {
                return new RelayCommand(obj => { MainWindow.Instance.Frame.Navigate(new View.Customers.Main(VMCustomer)); });
            }
        }

        public RelayCommand OpenEmployee
        {
            get
            {
                return new RelayCommand(obj => { MainWindow.Instance.Frame.Navigate(new View.Employees.Main(VMEmployee)); });
            }
        }
    }
}
