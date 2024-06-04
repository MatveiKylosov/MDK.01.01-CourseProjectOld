using System.Collections.Generic;
using System.Windows;

namespace MDK._01._01_CourseProject.View.Brands
{
    /// <summary>
    /// Логика взаимодействия для Filter.xaml
    /// </summary>
    public partial class Filter : Window
    {
        public string country;
        public string manufacture;
        public string address;

        private List<string> Countries;
        private List<string> Manufacturers;
        private List<string> Addresses;

        public Filter()
        {
            InitializeComponent();
        }

        public void SetupData(List<string> countries, List<string> manufacturers, List<string> addresses)
        {
            // Сохранение данных в приватные переменные
            Countries = countries;
            Manufacturers = manufacturers;
            Addresses = addresses;

            // Инициализация ComboBox
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            this.Country.Items.Clear();
            this.Manufacturer.Items.Clear();
            this.Address.Items.Clear();

            this.Country.Items.Add("Не выбран.");
            this.Manufacturer.Items.Add("Не выбран.");
            this.Address.Items.Add("Не выбран.");

            foreach (var item in Countries)
                this.Country.Items.Add(item);

            foreach (var item in Manufacturers)
                this.Manufacturer.Items.Add(item);

            foreach (var item in Addresses)
                this.Address.Items.Add(item);


            this.Country.SelectedItem = country;
            this.Manufacturer.SelectedItem = manufacture;
            this.Address.SelectedItem = address;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            country = this.Country.SelectedItem as string;
            manufacture = this.Manufacturer.SelectedItem as string;
            address = this.Address.SelectedItem as string;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public new bool? ShowDialog()
        {
            // Перед показом окна инициализируем ComboBox
            InitializeComboBoxes();
            base.ShowDialog();
            return ActiveFilter.IsChecked;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
