using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using Schema = System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MDK._01._01_CourseProject.Models
{
    public class Employee : NotMappedNotification
    {
        private string fullName;
        private int workExperience;
        private decimal salary;
        private string contactDetails;

        public int EmployeeID { get; set; }
        public string FullName
        {
            get { return fullName; }
            set
            {
                Match match = Regex.Match(value, "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    fullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }
        public int WorkExperience
        {
            get { return workExperience; }
            set
            {
                Match match = Regex.Match(value.ToString(), "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    workExperience = value;
                    OnPropertyChanged("WorkExperience");
                }
            }
        }
        public decimal Salary
        {
            get { return salary; }
            set
            {
                Match match = Regex.Match(value.ToString(), "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    salary = value;
                    OnPropertyChanged("Salary");
                }
            }
        }
        public string ContactDetails
        {
            get { return contactDetails; }
            set
            {
                Match match = Regex.Match(value, "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    contactDetails = value;
                    OnPropertyChanged("ContactDetails");
                }
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
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMEmployee.EmployeeContext.SaveChanges();
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
                    if (MessageBox.Show("Вы уверены что хотите удалить запись сотрудника?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var VMEmployee = (MainWindow.Instance.DataContext as ViewModels.VMPages).VMEmployee;
                        var VMCarSale = (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCarSale;

                        foreach (var carSale in VMCarSale.CarSale.Where(x => x.EmployeeID == this.EmployeeID).ToList())
                        {
                            VMCarSale.CarSale.Remove(carSale);
                            VMCarSale.CarSaleContext.Remove(carSale);
                        }
                        VMCarSale.CarSaleContext.SaveChanges();

                        VMEmployee.Employee.Remove(this);
                        VMEmployee.EmployeeContext.Remove(this);
                        VMEmployee.EmployeeContext.SaveChanges();
                    }
                }
                );
            }
        }
    }

}
