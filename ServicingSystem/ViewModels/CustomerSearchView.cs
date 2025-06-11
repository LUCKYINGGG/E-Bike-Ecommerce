
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicingSystem.ViewModels
{
    public class CustomerSearchView
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string ContactPhone { get; set; }

        public bool RemoveFromViewFlag { get; set; }

        //public List<CustomerVehicleView> CustomerVehicleView { get; set; }

    }
}
