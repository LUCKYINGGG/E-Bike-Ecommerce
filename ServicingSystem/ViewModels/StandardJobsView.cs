using ServicingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicingSystem.ViewModels
{
    public class StandardJobsView
    {
        public int StandardJobID { get; set; }

        //public int ServiceJobID { get; set; }

        public string Description { get; set; }
        public decimal StandardHours { get; set; }

        public string Comments { get; set; }

        public bool RemoveFromViewFlag { get; set; }
        public decimal ExtPrice { get; set; }
        public const decimal Rate = 65.50m;

        // The following example illustrates the MemberwiseClone method. It defines a ShallowCopy method that calls the MemberwiseClone method to perform a shallow copy operation on a Person object. 
        //public StandardJobsView ShallowCopy()
        //{
        //    return (StandardJobsView)MemberwiseClone();
        //}


    }
}
