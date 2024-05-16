using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.Context;
using MDK._01._01_CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDK._01._01_CourseProject.ViewModels
{
    public class VMCarSale
    {
        public CarSaleContext CarSaleContext = new CarSaleContext();
        public ObservableCollection<CarSale> CarSale { get; set; }
        public VMCarSale() 
        { 
            CarSale = new ObservableCollection<CarSale>(CarSaleContext.CarSales);
        }

        public RelayCommand AddCarSale
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    CarSale carSale = new CarSale();

                    this.CarSale.Add(carSale);
                    CarSaleContext.CarSales.Add(carSale);
                    CarSaleContext.SaveChanges();
                }
                );
            }
        }
    }
}
