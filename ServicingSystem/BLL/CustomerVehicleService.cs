#nullable disable

using ServicingSystem.DAL;
using ServicingSystem.Entities;
using ServicingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicingSystem.BLL
{
    public class CustomerVehicleService
    {
        #region Fields
        private readonly eBike_2025Context _eBike2025Context;

        // constructor
        internal CustomerVehicleService(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }

        #endregion

        public List<CustomerVehicleView> GetCustomerVehicles(int customerid)
        {
            if (customerid == 0)
            {
                throw new ArgumentNullException($"Please select a customer.");
            }

            return _eBike2025Context.CustomerVehicles.Where(cv => cv.CustomerID == customerid && !cv.RemoveFromViewFlag)
                                    .Select(cv => new CustomerVehicleView
                                    {
                                        Vin = cv.VehicleIdentification,
                                        CustomerID = cv.CustomerID,
                                        MakeModel = cv.Make.Trim() + ", " + cv.Model,
                                        RemoveFromViewFlag = cv.RemoveFromViewFlag
                                    }).ToList();
        }

        public CustomerVehicleView GetCustomerVehicle(string vin, int customerid)
        {
            if (customerid == 0)
            {
                throw new ArgumentNullException($"Please select a customer.");
            }
            if (string.IsNullOrWhiteSpace(vin))
            {
                throw new ArgumentNullException($"Please select a vehicle.");
            }

            return _eBike2025Context.CustomerVehicles
                    .Where(cv => cv.CustomerID == customerid && cv.VehicleIdentification.Equals(vin) && !cv.RemoveFromViewFlag)
                    .Select(cv => new CustomerVehicleView
                    {
                        Vin = cv.VehicleIdentification,
                        CustomerID = cv.CustomerID,
                        MakeModel = cv.Make.Trim() + ", " + cv.Model,
                        RemoveFromViewFlag = cv.RemoveFromViewFlag

                    }).FirstOrDefault();

        }
    }
}
