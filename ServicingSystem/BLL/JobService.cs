using ServicingSystem.DAL;
using ServicingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicingSystem.Entities;


namespace ServicingSystem.BLL
{
    public class JobService
    {
        #region Fields
        private readonly eBike_2025Context _eBike2025Context;

        // constructor
        internal JobService(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }


        #endregion

        #region Methods

        public JobView GetJobByJobId(int jobId)
        {
            if (jobId == 0)
            {
                throw new ArgumentNullException($"Please provide a job ID.");
            }

            return _eBike2025Context.Jobs.Where(j => j.JobID == jobId && !j.RemoveFromViewFlag)
                                            .Select(j => new JobView
                                            {
                                                JobId = j.JobID,
                                                JobDateIn = j.JobDateIn,
                                                JobDateStarted = j.JobDateStarted,
                                                JobDateDone = j.JobDateDone,
                                                JobDateOut = j.JobDateOut,
                                                EmployeeId = j.EmployeeID,
                                                ShopRate = j.ShopRate,
                                                VehicleIdentification = j.VehicleIdentification,
                                                CouponId = j.CouponID,
                                                RemoveFromViewFlag = j.RemoveFromViewFlag
                                            })
                                            .FirstOrDefault();

        }

        public JobView AddServiceJob(JobView jobView)
        {
            List<Exception> errorList = new List<Exception>();

            #region Business Logic and Parameter Exceptiions

            if (jobView == null)
            {
                throw new ArgumentNullException($"No service job was supply");
            }

            if (jobView.SelectedJobsList.Count == 0)
            {
                errorList.Add(new Exception("No service selected, please select at least one service to register service order."));
            }

            foreach (var item in jobView.SelectedJobsList)
            {
                if (item.StandardHours <= 0)
                {
                    errorList.Add(new Exception($"{item.Description} service hour cannot be less than or equal to 0."));
                }

                if (item.ExtPrice <= 0)
                {
                    errorList.Add(new Exception($"{item.Description} service expected price cannot be less than or equal to 0."));
                }

            }

            foreach (var item in jobView.SelectedPartsList)
            {
                if (item.Qty > item.QuantityOnHand)
                {
                    errorList.Add(new Exception($"The selected quantity of {item.Description} is greater than the quantity on hand."));
                }

                if (item.Qty <= 0)
                {
                    errorList.Add(new Exception($"The selected quantity of {item.Description} is a negative or zero quantity."));
                }

                if (item.ExtPrice <= 0)
                {
                    errorList.Add(new Exception($"The selected {item.Description} has a negative expected price."));
                }

            }

            #endregion

            Job job = new();

            // job
            job.JobDateIn = jobView.JobDateIn;
            job.EmployeeID = jobView.EmployeeId;
            job.ShopRate = jobView.ShopRate;
            job.VehicleIdentification = jobView.VehicleIdentification;
            job.CouponID = jobView.CouponId;
            job.RemoveFromViewFlag = jobView.RemoveFromViewFlag;

            // job detail
            foreach (var item in jobView.SelectedJobsList)
            {
                JobDetail jobDetail = new();

                jobDetail.JobID = jobView.JobId;
                jobDetail.Description = item.Description;
                jobDetail.JobHours = item.StandardHours;
                jobDetail.Comments = item.Comments;

                if (jobView.JobDateDone != null)
                {
                    jobDetail.StatusCode = "D";
                }
                else if (jobView.JobDateStarted != null)
                {
                    jobDetail.StatusCode = "S";
                }
                else
                {
                    jobDetail.StatusCode = "I";
                }

                jobDetail.EmployeeID = jobView.EmployeeId;
                jobDetail.RemoveFromViewFlag = item.RemoveFromViewFlag;

                job.JobDetails.Add(jobDetail);

            }

            // job part
            
            foreach (var item in jobView.SelectedPartsList)
            {
                JobPart jobPart = new();

                jobPart.JobID = jobView.JobId;
                jobPart.PartID = item.PartID;
                jobPart.Quantity = item.Qty;
                jobPart.SellingPrice = item.SellingPrice;
                jobPart.RemoveFromViewFlag = item.RemoveFromViewFlag;

                job.JobParts.Add(jobPart);

                // update part tables
                Part part = _eBike2025Context.Parts.Where(p => p.PartID == item.PartID).FirstOrDefault();

                if (part != null)
                {
                    part.QuantityOnHand -= item.Qty;
                    _eBike2025Context.Update(part);
                }
                else
                {
                    errorList.Add(new Exception($"Part {item.Description} does not exist."));
                }
            }

            _eBike2025Context.Jobs.Add(job);

            if (errorList.Count > 0)
            {
                _eBike2025Context.ChangeTracker.Clear();

                throw new AggregateException("Unable to register service order. Please check error message(s)", errorList);

            }
            else
            {
                try
                {
                    _eBike2025Context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception($"Error while saving: {ex.Message}");
                }
            }

            return GetJobByJobId(job.JobID);
        }

        #endregion







    }
}
