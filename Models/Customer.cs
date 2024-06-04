using MDK._01._01_CourseProject.Common;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace MDK._01._01_CourseProject.Models
{
    public class Customer : NotMappedNotification
    {
        private string fullName;
        private string passportData;
        private string address;
        private DateTime? birthDate;
        private bool gender;
        private string contactDetails;

        public int CustomerID { get; set; }
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
        public string PassportData
        {
            get { return passportData; }
            set
            {
                Match match = Regex.Match(value, @"\d{10}");
                if (match.Success)
                {
                    passportData = value;
                    OnPropertyChanged("PassportData");
                }
                else
                    MessageBox.Show($"Ошибка", "Данные паспорта вводятся в формате 10 цифр серии и номера без промежутков.");

            }
        }
        public string Address
        {
            get { return address; }
            set
            {
                if (value.Length > 0 && value.Length <= 255)
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
                else
                    MessageBox.Show($"Ошибка", "Адрес должен быть больше 0 и меньше 255 символов.");
            }
        }
        public DateTime? BirthDate
        {
            get { return birthDate; }
            set
            {
                if (!value.HasValue)
                    return;

                Match match = Regex.Match(value.Value.ToString("dd.MM.yyyy"), @"(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[012])\.\d{4}");
                if (match.Success)
                {
                    birthDate = value;
                    OnPropertyChanged("BirthDate");
                }
                else
                    MessageBox.Show($"Ошибка", "Укажите дату в формате - \"дд.мм.гггг\".");
            }
        }
        public bool? Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = !value.HasValue ? false : value.Value;
                OnPropertyChanged("Gender");
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
                    var result = MessageBox.Show("Удалить вместе с этой записью другие?", "Предупреждение", MessageBoxButton.YesNoCancel);

                    if (result == MessageBoxResult.Yes || result == MessageBoxResult.No)
                    {
                        var VMPages = MainWindow.Instance.DataContext as ViewModels.VMPages;
                        var VMCarSale = VMPages.VMCarSale;
                        var VMCustomer = VMPages.VMCustomer;

                        var CarSaleList = VMCarSale.CarSale.Where(x => x.CustomerID == this.CustomerID).ToList();

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
                                carSale.CarID = null;
                                VMCarSale.CarSaleContext.CarSales.First(x => carSale.SaleID == x.SaleID).CustomerID = null;
                            }
                        }

                        VMCustomer.Customer.Remove(this);
                        VMCustomer.CustomerContext.Remove(this);

                        VMCarSale.CarSaleContext.SaveChanges();
                        VMCustomer.CustomerContext.SaveChanges();
                    }
                });
            }
        }
    }
}
