using MDK._01._01_CourseProject.Common;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using Schema = System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MDK._01._01_CourseProject.Models
{
    public class Customer : NotMappedNotification
    {
        private string fullName;
        private string passportData;
        private string address;
        private DateTime birthDate;
        private string gender;
        private string contactDetails;

        public int CustomerID { get; set; }
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
        public string PassportData
        {
            get { return passportData; }
            set
            {
                Match match = Regex.Match(value, "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    passportData = value;
                    OnPropertyChanged("");
                }
            }
        }
        public string Address
        {
            get { return address; }
            set
            {
                Match match = Regex.Match(value, "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    address = value;
                    OnPropertyChanged("");
                }
            }
        }
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged("");
            }
        }
        public string Gender
        {
            get { return gender; }
            set
            {
                Match match = Regex.Match(value, "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    gender = value;
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
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCustomer.CustomerContext.SaveChanges();
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
                    if (MessageBox.Show("Вы уверены что хотите удалить запись клиента?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var VMCarSale = (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCarSale;
                        var VMCustomer = (MainWindow.Instance.DataContext as ViewModels.VMPages).VMCustomer;
                        foreach (var carSale in VMCarSale.CarSale.Where(x => x.CustomerID == this.CustomerID).ToList())
                        {
                            VMCarSale.CarSale.Remove(carSale);
                            VMCarSale.CarSaleContext.Remove(carSale);
                        }
                        VMCarSale.CarSaleContext.SaveChanges();

                        VMCustomer.Customer.Remove(this);
                        VMCustomer.CustomerContext.Remove(this);
                        VMCustomer.CustomerContext.SaveChanges();
                    }
                });
            }
        }

    }
}
