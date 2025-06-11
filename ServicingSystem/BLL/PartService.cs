using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicingSystem.DAL;
using ServicingSystem.ViewModels;

namespace ServicingSystem.BLL
{
    public class PartService
    {
        #region Fields
        private readonly eBike_2025Context _eBike2025Context;

        // constructor
        internal PartService(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }

        #endregion

        #region methods

        public List<PartView> GetParts(int categoryid)
        {
            if (categoryid == 0)
            {
                throw new ArgumentNullException($"Please select a category.");
            }

            return _eBike2025Context.Parts.Where(p => p.CategoryID == categoryid && !p.RemoveFromViewFlag)
                                            .Select(p => new PartView
                                                {
                                                    PartID = p.PartID,
                                                    Description = p.Description,
                                                    SellingPrice = p.SellingPrice,
                                                    QuantityOnHand = p.QuantityOnHand,
                                                    ReorderLevel = p.ReorderLevel,
                                                    QuantityOnOrder = p.QuantityOnOrder,
                                                    Refundable = p.Refundable,
                                                    Discontinued = p.Discontinued,
                                                    VendorID = p.VendorID,
                                                    RemoveFromViewFlag = p.RemoveFromViewFlag,
                                                    CategoryID = p.CategoryID

                                                }).ToList();
        }


        public PartView GetPart(int partID)
        {
            if (partID == 0)
            {
                throw new ArgumentNullException($"Please select a part.");
            }

            return _eBike2025Context.Parts.Where(p => p.PartID == partID && !p.RemoveFromViewFlag)
                .Select(p => new PartView
                {
                    Description = p.Description,
                    SellingPrice = p.SellingPrice,
                    QuantityOnHand = p.QuantityOnHand,
                    ReorderLevel = p.ReorderLevel,
                    QuantityOnOrder = p.QuantityOnOrder,
                    CategoryID = p.CategoryID,
                    Refundable = p.Refundable,
                    Discontinued = p.Discontinued,
                    VendorID = p.VendorID,
                    RemoveFromViewFlag = p.RemoveFromViewFlag

                }).FirstOrDefault();
        }


        #endregion

    }
}
