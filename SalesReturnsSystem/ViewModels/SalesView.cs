using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReturnsSystem.ViewModels
{
    public class SalesView
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public bool RemoveFromViewFlag { get; set; }
    }
}
