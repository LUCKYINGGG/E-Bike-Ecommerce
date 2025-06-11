using SalesReturnsSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesReturnsSystem.ViewModels;

namespace SalesReturnsSystem.BLL
{
    public class CustomerSaleServices
    {
        #region Fields
        private readonly eBike_2025Context _eBike2025Context;

        internal CustomerSaleServices(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }
        #endregion

        public List<CustomerPartsView> GetCustomerOrders()
        {
            return _eBike2025Context.SaleDetails.Where(x => !x.RemoveFromViewFlag)
                                .Select(x => new CustomerPartsView
                                {
                                    SaleDetailID = x.SaleDetailID,
                                    SaleID = x.SaleID,
                                    PartID = x.PartID,
                                    Quantity = x.Quantity,
                                    SellingPrice = x.SellingPrice,
                                    RemoveFromViewFlag = x.RemoveFromViewFlag
                                }).ToList();

        }


        public CustomerPartsView? GetCustomerOrder(int saleID)
        {
            return _eBike2025Context.SaleDetails.Where(x => x.SaleID == saleID && !x.RemoveFromViewFlag)
                                .Select(x => new CustomerPartsView
                                {
                                    SaleDetailID = x.SaleDetailID,
                                    SaleID = x.SaleID,
                                    PartID = x.PartID,
                                    Quantity = x.Quantity,
                                    RemoveFromViewFlag = x.RemoveFromViewFlag
                                }).FirstOrDefault();
        }
    }
}
