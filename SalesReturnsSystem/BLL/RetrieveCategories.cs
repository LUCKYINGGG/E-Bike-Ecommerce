using SalesReturnsSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesReturnsSystem.ViewModels;

namespace SalesReturnsSystem.BLL
{
    public class RetrieveCategories
    {
        #region Fields
        private readonly eBike_2025Context _eBike2025Context;

        // constructor
        internal RetrieveCategories(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }

        #endregion

        #region method

        public List<CategoriesView> GetCategories()
        {
            return _eBike2025Context.Categories.Where(x => !x.RemoveFromViewFlag)
                .Select(x => new CategoriesView
                {
                    CategoryID = x.CategoryID,
                    Description = x.Description,
                    RemoveFromViewFlag = x.RemoveFromViewFlag
                }).ToList();
        }

        #endregion
    }
}
