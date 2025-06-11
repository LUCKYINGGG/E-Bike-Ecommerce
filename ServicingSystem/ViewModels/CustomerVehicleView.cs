using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicingSystem.ViewModels
{
    public class CustomerVehicleView
    {
        public string Vin { get; set; }
        public int CustomerID { get; set; }
        public string MakeModel { get; set; }
        public bool RemoveFromViewFlag { get; set; }

    }
}
