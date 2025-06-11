using SalesReturnsSystem.DAL;
using SalesReturnsSystem.Entities;
using SalesReturnsSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReturnsSystem.BLL
{
    public class SalesServices
    {
        #region Fields
        private readonly eBike_2025Context _eBike2025Context;

        // constructor
        internal SalesServices(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }

        #endregion

        #region methods

        public List<SalesView> GetSales(int saleid)
        {
            if (saleid == 0)
            {
                throw new ArgumentNullException($"No records of a sale found.");
            }

            return _eBike2025Context.Sales.Where(x => x.SaleID == saleid && !x.RemoveFromViewFlag)
                                            .Select(x => new SalesView
                                            {
                                                SaleID = x.SaleID,
                                                SaleDate = x.SaleDate,
                                                CustomerID = x.CustomerID,
                                            RemoveFromViewFlag = x.RemoveFromViewFlag

                                            }).ToList();
        }


        public SalesView? GetSale(int customerID)
        {
            return _eBike2025Context.Sales.Where(x => x.CustomerID == customerID && !x.RemoveFromViewFlag)
                                            .Select(x => new SalesView
                                            {
                                                SaleID = x.SaleID,
                                                SaleDate = x.SaleDate,
                                                CustomerID = x.CustomerID,
                                                RemoveFromViewFlag = x.RemoveFromViewFlag

                                            }).FirstOrDefault();
        }


        #endregion
    }
}
