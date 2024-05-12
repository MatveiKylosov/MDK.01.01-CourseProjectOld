using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.Context;
using MDK._01._01_CourseProject.Models;
using System.Collections.ObjectModel;

namespace MDK._01._01_CourseProject.ViewModels
{
    public class VMBrand
    {
        public BrandContext BrandContext = new BrandContext();
        public ObservableCollection<Brand> Brand {  get; set; }
        public VMBrand()
        {
            Brand = new ObservableCollection<Brand>(BrandContext.Brands);
        }
        public RelayCommand AddBrand { get
            {
                return new RelayCommand(obj =>
                    { 
                        Brand brand = new Brand();

                        this.Brand.Add(brand);
                        BrandContext.Brands.Add(brand);
                        BrandContext.SaveChanges();
                    }
                );
            }
        }
    }
}
