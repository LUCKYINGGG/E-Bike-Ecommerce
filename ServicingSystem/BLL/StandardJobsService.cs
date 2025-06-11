#nullable disable

using ServicingSystem.DAL;
using ServicingSystem.Entities;
using ServicingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicingSystem.BLL
{
    public class StandardJobsService
    {
        #region Fields
        private readonly eBike_2025Context _eBike2025Context;

        // constructor
        internal StandardJobsService(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }
        #endregion

        public List<StandardJobsView> GetStandardJobs()
        {
            return _eBike2025Context.StandardJobs.Where(sj => !sj.RemoveFromViewFlag)
                                .Select(sj => new StandardJobsView
                                {
                                    StandardJobID = sj.StandardJobID,
                                    Description = sj.Description,
                                    StandardHours = sj.StandardHours,
                                    ExtPrice = sj.StandardHours * 65.5m,
                                    RemoveFromViewFlag = sj.RemoveFromViewFlag
                                }).ToList();

        }


        public StandardJobsView GetStandardJob(int standardJobID)
        {
            if (standardJobID == 0)
            {
                throw new ArgumentNullException($"Please select a job.");
            }

            return _eBike2025Context.StandardJobs.Where(sj => sj.StandardJobID == standardJobID && !sj.RemoveFromViewFlag)
                                .Select(sj => new StandardJobsView
                                {
                                    StandardJobID = sj.StandardJobID,
                                    Description = sj.Description,
                                    StandardHours = sj.StandardHours,
                                    ExtPrice = 65.5m * sj.StandardHours,
                                    RemoveFromViewFlag = sj.RemoveFromViewFlag
                                }).FirstOrDefault();
        }


        

    }
}
