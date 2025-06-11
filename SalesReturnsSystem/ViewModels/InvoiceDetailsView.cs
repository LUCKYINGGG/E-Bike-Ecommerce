using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReturnsSystem.ViewModels
{
    public class InvoiceDetailsView
    {
        public int SaleRefundDetailID { get; set; }
        public int SalesreturnID { get; set; }
        public int PartID { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public string Reason { get; set; }
        public bool RemoveFromViewFlag { get; set; }
    }
}
