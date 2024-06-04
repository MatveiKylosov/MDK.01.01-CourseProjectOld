using OfficeOpenXml;
using System.Windows;

namespace MDK._01._01_CourseProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public MainWindow()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            InitializeComponent();
            Instance = this;
            DataContext = new ViewModels.VMPages();
        }
    }
}
