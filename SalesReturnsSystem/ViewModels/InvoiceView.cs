using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReturnsSystem.ViewModels
{
    public class InvoiceView
    {
        public int SalesreturnID { get; set; }
        public DateTime SalesreturnDate { get; set; }
        public int SaleID { get; set; }
        public int EmployeeID { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal SubTotal { get; set; }
        public bool RemoveFromViewFlag { get; set; }
    }
}
