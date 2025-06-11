using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicingSystem.DAL;
using ServicingSystem.ViewModels;

namespace ServicingSystem.BLL
{
    public class CouponService
    {
        #region Fields
        private readonly eBike_2025Context _eBike2025Context;

        // constructor
        internal CouponService(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }

        #endregion


        public CouponView GetCouponByCouponValue(string couponValue)
        {
            if (string.IsNullOrWhiteSpace(couponValue))
            {
                throw new ArgumentNullException($"Please provide a coupon.");
            }

            return _eBike2025Context.Coupons.Where(c => c.CouponIDValue == couponValue && !c.RemoveFromViewFlag
                                                     && DateTime.Today >= c.StartDate && DateTime.Today <= c.EndDate)
                                            .Select(c => new CouponView
                                            {
                                                CouponID = c.CouponID,
                                                CouponIDValue = c.CouponIDValue,
                                                StartDate = c.StartDate,
                                                EndDate = c.EndDate,
                                                CouponDiscount = c.CouponDiscount,
                                                SalesOrService = c.SalesOrService,
                                                RemoveFromViewFlag = c.RemoveFromViewFlag
                                            })
                                            .FirstOrDefault();
        }

    }
}
