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
        private int? workExperience;
        private decimal? salary;
        private string contactDetails;

        public int EmployeeID { get; set; }
        public string FullName
        {
            get { return fullName; }
            set
            {
                if (value.Length > 0 && value.Length <= 255)
                {
                    fullName = value;
                    OnPropertyChanged("FullName");
                }
                else
                    MessageBox.Show($"Ошибка", "Название машины должно быть больше 0 и меньше 255 символов.");
            }
        }
        public int? WorkExperience
        {
            get { return workExperience; }
            set
            {
                Match match = Regex.Match(value.ToString(), @"^\d+$");
                if (!match.Success)
                    MessageBox.Show("Ошибка", "Стаж работы должен быть положительным числом.");
                else
                {
                    workExperience = value;
                    OnPropertyChanged("WorkExperience");
                }
            }
        }

        public decimal? Salary
        {
            get { return salary; }
            set
            {
                Match match = Regex.Match(value.ToString(), @"^\d*[\.,]?\d*$");
                if (!match.Success)
                    MessageBox.Show("Ошибка", "Зарплата должна быть положительным числом.");
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
                if (value.Length > 0 && value.Length <= 255)
                {
                    contactDetails = value;
                    OnPropertyChanged("ContactDetails");
                }
                else
                    MessageBox.Show($"Ошибка", "Контактные данные должны быть больше 0 и меньше 255 символов.");
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
                    var result = MessageBox.Show("Удалить вместе с этой записью другие?", "Предупреждение", MessageBoxButton.YesNoCancel);

                    if (result == MessageBoxResult.Yes || result == MessageBoxResult.No)
                    {
                        var VMPages = MainWindow.Instance.DataContext as ViewModels.VMPages;
                        var VMEmployee  = VMPages.VMEmployee;
                        var VMCarSale   = VMPages.VMCarSale;

                        var CarSaleList = VMCarSale.CarSale.Where(x => x.EmployeeID == this.EmployeeID).ToList();

                        if (result == MessageBoxResult.Yes)
                        {
                            foreach (var carSale in CarSaleList)
                            {
                                VMCarSale.CarSale.Remove(carSale);
                                VMCarSale.CarSaleContext.Remove(carSale);
                            }
                        }
                        else
                        {
                            foreach (var carSale in CarSaleList)
                            {
                                carSale.EmployeeID = null;
                                VMCarSale.CarSaleContext.CarSales.First(x => carSale.SaleID == x.SaleID).EmployeeID = null;
                            }
                        }

                        VMEmployee.Employee.Remove(this);
                        VMEmployee.EmployeeContext.Remove(this);

                        VMCarSale.CarSaleContext.SaveChanges();
                        VMEmployee.EmployeeContext.SaveChanges();
                    }
                }
                );
            }
        }
    }
}
