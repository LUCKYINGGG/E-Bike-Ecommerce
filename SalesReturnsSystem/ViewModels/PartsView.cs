using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReturnsSystem.ViewModels
{
    public class PartsView
    {
        public int PartID { get; set; }
        public string Description { get; set; }
        public decimal SellingPrice { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReorderLevel { get; set; }
        public int QuantityOnOrder { get; set; }
        public int CategoryID { get; set; }
        public string Refundable { get; set; }
        public bool Discontinued { get; set; }
        public int VendorID { get; set; }
        public bool RemoveFromViewFlag { get; set; }
        public int Qty { get; set; }

        public decimal ExtPrice
        {
            get
            {
                return this.SellingPrice * this.Qty;
            }

        }

        public PartsView ShallowCopy()
        {
            return (PartsView)MemberwiseClone();
        }
    }
}
