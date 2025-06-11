using SalesReturnsSystem.DAL;
using SalesReturnsSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReturnsSystem.BLL
{
    public class CustomerServices
    {
        #region Cosntructors
        private readonly eBike_2025Context _eBike2025Context;

        internal CustomerServices(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }

        #endregion

        #region Methods
        public List<CustomerSalesSearchView> GetCustomers(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentNullException("Please provide a phone number");
            }
            return _eBike2025Context.Customers
                .Where(x => x.ContactPhone.Contains(phoneNumber) && !x.RemoveFromViewFlag)
                .Select(x => new CustomerSalesSearchView
                {
                    CustomerID = x.CustomerID,
                    FullName = x.FirstName + " " + x.LastName,
                    PhoneNumber = x.ContactPhone,
                    Address = x.Address,
                    RemoveFromViewFlag = x.RemoveFromViewFlag
                }).ToList();
        }

        public CustomerSalesSearchView? GetCustomer(int customerID)
        {
            return _eBike2025Context.Customers.Where(x => x.CustomerID == customerID && !x.RemoveFromViewFlag)
                .Select(x => new CustomerSalesSearchView
                {
                    CustomerID = x.CustomerID,
                    FullName = x.FirstName + " " + x.LastName,
                    PhoneNumber = x.ContactPhone,
                    Address = x.Address,
                    RemoveFromViewFlag = x.RemoveFromViewFlag
                }).FirstOrDefault();

        }
        #endregion
    }
}
