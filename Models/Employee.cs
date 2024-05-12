using MDK._01._01_CourseProject.Common;
using System.Text.RegularExpressions;
using System.Windows;
using Schema = System.ComponentModel.DataAnnotations.Schema;

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
                    OnPropertyChanged("");
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
                    OnPropertyChanged("");
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
                    OnPropertyChanged("");
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
                    OnPropertyChanged("");
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
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMEmployee.Employee.Remove(this);
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMEmployee.EmployeeContext.Remove(this);
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMEmployee.EmployeeContext.SaveChanges();
                    }
                }
                );
            }
        }
    }

}
