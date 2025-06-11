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
    public class InvoiceServices
    {
        #region Cosntructors
        private readonly eBike_2025Context _eBike2025Context;

        internal InvoiceServices(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }

        #endregion

        #region Methods
        public List<InvoiceView> GetInvoices(int salerefundID)
        {
            return _eBike2025Context.SaleRefunds
                .Where(x => x.SaleRefundID == salerefundID && !x.RemoveFromViewFlag)
                .Select(x => new InvoiceView
                {
                    SalesreturnID = x.SaleRefundID,
                    SalesreturnDate = x.SaleRefundDate,
                    SaleID = x.SaleID,
                    TaxAmount = x.TaxAmount,
                    SubTotal = x.SubTotal,
                    RemoveFromViewFlag = x.RemoveFromViewFlag
                }).ToList();
        }

        public InvoiceView? GetInvoice(int saleRefundID)
        {
            return _eBike2025Context.SaleRefunds.Where(x => x.SaleRefundID == saleRefundID && !x.RemoveFromViewFlag)
                .Select(x => new InvoiceView
                {
                    SalesreturnID = x.SaleRefundID,
                    SalesreturnDate = x.SaleRefundDate,
                    SaleID = x.SaleID,
                    TaxAmount = x.TaxAmount,
                    SubTotal = x.SubTotal,
                    RemoveFromViewFlag = x.RemoveFromViewFlag
                }).FirstOrDefault();

        }
        #endregion
    }
}
