using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicingSystem.ViewModels
{
    public class JobView
    {
        public int JobId { get; set; }
        public DateTime JobDateIn { get; set; }
        public DateTime? JobDateStarted { get; set; }
        public DateTime? JobDateDone { get; set; }
        public DateTime? JobDateOut { get; set; }
        public string EmployeeId { get; set; }

        public decimal ShopRate { get; set; }
        public string VehicleIdentification { get; set; }
        public int? CouponId { get; set; }
        public bool RemoveFromViewFlag { get; set; }

        public List<StandardJobsView> SelectedJobsList { get; set; } = new();

        public List<PartView> SelectedPartsList { get; set; } = new();

    }
}
