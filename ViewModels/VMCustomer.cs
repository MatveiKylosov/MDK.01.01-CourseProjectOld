using MDK._01._01_CourseProject.Common;
using MDK._01._01_CourseProject.Context;
using MDK._01._01_CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDK._01._01_CourseProject.ViewModels
{
    public class VMCustomer
    {
        public CustomerContext CustomerContext { get; set; }
        public ObservableCollection<Customer> Customer { get; set; }
        public VMCustomer()
        {
            Customer = new ObservableCollection<Customer>(CustomerContext.Customers);
        }

        public RelayCommand AddCustomer
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Customer Customer = new Customer();

                    this.Customer.Add(Customer);
                    CustomerContext.Customers.Add(Customer);
                    CustomerContext.SaveChanges();
                }
                );
            }
        }
    }
}
