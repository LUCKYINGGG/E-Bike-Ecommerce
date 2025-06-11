using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicingSystem.DAL;
using ServicingSystem.ViewModels;

namespace ServicingSystem.BLL
{
    public class CategoryService
    {
        #region Fields
        private readonly eBike_2025Context _eBike2025Context;

        // constructor
        internal CategoryService(eBike_2025Context eBike2025Context)
        {
            _eBike2025Context = eBike2025Context;
        }

        #endregion

        #region method

        public List<CategoryView> GetCategories()
        {
            return _eBike2025Context.Categories.Where(c => !c.RemoveFromViewFlag)
                .Select(c => new CategoryView
                {
                    CategoryID = c.CategoryID,
                    Description = c.Description,
                    RemoveFromViewFlag = c.RemoveFromViewFlag
                }).ToList();
        }

        #endregion


    }
}
