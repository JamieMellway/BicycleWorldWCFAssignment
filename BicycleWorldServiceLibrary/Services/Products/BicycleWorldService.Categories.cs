using System.Collections.Generic;
using System.Security.Permissions;
using BicycleWorldCore;
using BicycleWorldEntityModel;

namespace BicycleWorldServiceLibrary
{
    public partial class BicycleWorldService : IBicycleWorldService
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public ProductCategory GetCategory(int categoryID)
        {
            return CategoryCore.GetCategory(categoryID);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public List<ProductCategory> CategoriesList()
        {
            return CategoryCore.CategoriesList();
        }

        [PrincipalPermission(SecurityAction.Demand,Role="Admin")]
        public int AddCategory(ProductCategory category)
        {
            return CategoryCore.AddCategory(category);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public bool RemoveCategory(int categoryID)
        {
            return CategoryCore.RemoveCategory(categoryID);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public ProductCategory UpdateCategory(ProductCategory category)
        {
            return CategoryCore.UpdateCategory(category);
        }
    }
}