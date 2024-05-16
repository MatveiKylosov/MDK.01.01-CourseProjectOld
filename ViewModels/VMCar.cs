using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.Context;
using MDK._01._01_CourseProject.Models;
using System.Collections.ObjectModel;
namespace MDK._01._01_CourseProject.ViewModels
{
    public class VMCar
    {
        public CarContext CarContext = new CarContext();
        public ObservableCollection<Car> Car { get; set; }
        public VMCar()
        {
            Car = new ObservableCollection<Car>(CarContext.Cars);
        }

        public RelayCommand AddCar
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Car car = new Car();

                    this.Car.Add(car);
                    CarContext.Cars.Add(car);
/*                    CarContext.SaveChanges();*/
                }
                );
            }
        }
    }
}
