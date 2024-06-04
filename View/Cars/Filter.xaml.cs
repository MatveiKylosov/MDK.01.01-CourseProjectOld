using MDK._01._01_CourseProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MDK._01._01_CourseProject.View.Cars
{
    /// <summary>
    /// Логика взаимодействия для Filter.xaml
    /// </summary>
    public partial class Filter : Window
    {
        public Filter()
        {
            InitializeComponent();
        }

        public void SetupData(List<Brand> brands, List<Car> cars)
        {
            this.Brand.Items.Add("Не выбран.");
            this.Color.Items.Add("Не выбран.");
            this.Category.Items.Add("Не выбран.");

            foreach (var brand in brands)
                this.Brand.Items.Add(brand.BrandName);

            var uniqueColors = cars.Select(car => car.Color).Distinct().ToList();

            this.Color.ItemsSource = uniqueColors;

        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
