using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesReturnsSystem.ViewModels;
using SalesReturnsSystem.DAL;

namespace SalesReturnsSystem.BLL
{
    public class PartsServices
    {
        #region Fields
        private readonly eBike_2025Context _eBike2025Context;

        // constructor
        internal PartsServices(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }

        #endregion

        #region methods

        public List<PartsView> GetParts(int categoryid)
        {
            if (categoryid == 0)
            {
                throw new ArgumentNullException($"Please select a category.");
            }

            return _eBike2025Context.Parts.Where(x => x.CategoryID == categoryid && !x.RemoveFromViewFlag)
                                            .Select(x => new PartsView
                                            {
                                                PartID = x.PartID,
                                                Description = x.Description,
                                                SellingPrice = x.SellingPrice,
                                                QuantityOnHand = x.QuantityOnHand,
                                                ReorderLevel = x.ReorderLevel,
                                                QuantityOnOrder = x.QuantityOnOrder,
                                                Refundable = x.Refundable,
                                                Discontinued = x.Discontinued,
                                                VendorID = x.VendorID,
                                                
                                                RemoveFromViewFlag = x.RemoveFromViewFlag,
                                                CategoryID = x.CategoryID

                                            }).ToList();
        }


        public PartsView? GetPart(int partID)
        {
            return _eBike2025Context.Parts.Where(x => x.PartID == partID && !x.RemoveFromViewFlag)
                .Select(x => new PartsView
                {
                    Description = x.Description,
                    SellingPrice = x.SellingPrice,
                    QuantityOnHand = x.QuantityOnHand,
                    ReorderLevel = x.ReorderLevel,
                    QuantityOnOrder = x.QuantityOnOrder,
                    CategoryID = x.CategoryID,
                    Refundable = x.Refundable,
                    Discontinued = x.Discontinued,
                    VendorID = x.VendorID,
                    RemoveFromViewFlag = x.RemoveFromViewFlag

                }).FirstOrDefault();
        }


        #endregion
    }
}
