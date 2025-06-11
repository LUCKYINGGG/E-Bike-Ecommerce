using SalesReturnsSystem.DAL;
using SalesReturnsSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReturnsSystem.BLL
{
    public class InvoiceDetailServices
    {

        #region Cosntructors
        private readonly eBike_2025Context _eBike2025Context;

        internal InvoiceDetailServices(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }

        #endregion

        #region Methods
        public List<InvoiceDetailsView> GetInvoiceDetails(int salerefundID)
        {
            return _eBike2025Context.SaleRefundDetails
                .Where(x => x.SaleRefundID == salerefundID && !x.RemoveFromViewFlag)
                .Select(x => new InvoiceDetailsView
                {
                    SaleRefundDetailID = x.SaleRefundDetailID,
                    SalesreturnID = x.SaleRefundID,
                    PartID = x.PartID,
                    Quantity = x.Quantity,
                    SellingPrice = x.SellingPrice,
                    Reason = x.Reason,
                    RemoveFromViewFlag = x.RemoveFromViewFlag
                }).ToList();
        }

        public InvoiceDetailsView? GetInvoice(int saleRefundID)
        {
            return _eBike2025Context.SaleRefundDetails.Where(x => x.SaleRefundID == saleRefundID && !x.RemoveFromViewFlag)
                .Select(x => new InvoiceDetailsView
                {
                    SaleRefundDetailID = x.SaleRefundDetailID,
                    SalesreturnID = x.SaleRefundID,
                    PartID = x.PartID,
                    Quantity = x.Quantity,
                    SellingPrice = x.SellingPrice,
                    Reason = x.Reason,
                    RemoveFromViewFlag = x.RemoveFromViewFlag
                }).FirstOrDefault();

        }
        #endregion
    }
}
