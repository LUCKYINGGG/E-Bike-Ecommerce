

using ServicingSystem.DAL;
using ServicingSystem.ViewModels;
using System.Numerics;


namespace ServicingSystem.BLL
{
    public class CustomerService
    {

        #region Fields
        private readonly eBike_2025Context _eBike2025Context;

        // constructor
        internal CustomerService(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }

        #endregion

        #region methods


        public List<CustomerSearchView> GetCustomers(string lastname)
        {
            // Business rules

            if (string.IsNullOrWhiteSpace(lastname))
            {
                throw new ArgumentNullException($"Please provide either a last name");
            }

            return _eBike2025Context.Customers
                                        .Where(x => x.LastName.ToLower().Contains(lastname.ToLower()) && !x.RemoveFromViewFlag)
                                        .OrderBy(x => x.LastName)
                                        .Select(x => new CustomerSearchView
                                        {
                                            CustomerID = x.CustomerID,

                                            Name = x.FirstName + " " + x.LastName,
                                            ContactPhone = x.ContactPhone,
                                            Address = x.Address,
                                            RemoveFromViewFlag = x.RemoveFromViewFlag,

                                        })
                                        .ToList();
        }

        public CustomerSearchView GetCustomer(int customerid)
        {
            // add validation for customerid
            if (customerid == 0)
            {
                throw new ArgumentNullException($"Customer ID is 0.");
            }

            return _eBike2025Context.Customers.Where(c => c.CustomerID == customerid && !c.RemoveFromViewFlag)
                                .Select(c => new CustomerSearchView
                                {
                                    CustomerID = c.CustomerID,
                                    Name = c.FirstName + " " + c.LastName,
                                    Address = c.Address,
                                    ContactPhone = c.ContactPhone,
                                    RemoveFromViewFlag = c.RemoveFromViewFlag
                                }).FirstOrDefault();

        }




        #endregion

    }
}
