using MDK._01._01_CourseProject.Common;
using System.Text.RegularExpressions;
using System.Windows;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace MDK._01._01_CourseProject.Models
{
    public class Brand : NotMappedNotification
    {
        private string brandName;
        private string country;
        private string manufacturer;
        private string address;

        public int BrandID { get; set; }
        public string BrandName 
        { 
            get{ return brandName; } 
            set
            {
                Match match = Regex.Match(value, "");
                if(!match.Success)
                    MessageBox.Show($"");
                else
                {
                    brandName = value;
                    OnPropertyChanged("");
                }
            }
        }
        public string Country 
        { 
            get{ return country; } 
            set
            {
                Match match = Regex.Match(value, "");
                if(!match.Success)
                    MessageBox.Show($"");
                else
                {
                    country = value;
                    OnPropertyChanged("");
                }
            }
        }
        public string Manufacturer 
        { 
            get{ return manufacturer; } 
            set
            {
                Match match = Regex.Match(value, "");
                if (!match.Success)
                    MessageBox.Show($"");
                else
                {
                    manufacturer = value;
                    OnPropertyChanged("");
                }
            }
        }
        public string Address 
        { 
            get{ return address; } 
            set
            {
                Match match = Regex.Match(value, "");
                if(!match.Success)
                    MessageBox.Show($"");
                else
                {
                    address = value;
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
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMBrand.BrandContext.SaveChanges();
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
                    if (MessageBox.Show("Вы уверены что хотите удалить запись бренда?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMBrand.Brand.Remove(this);
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMBrand.BrandContext.Remove(this);
                        (MainWindow.Instance.DataContext as ViewModels.VMPages).VMBrand.BrandContext.SaveChanges();
                    }
                }
                );
            }
        }
    }
}
