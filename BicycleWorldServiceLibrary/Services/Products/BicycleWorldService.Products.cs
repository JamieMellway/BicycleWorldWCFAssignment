using System.Collections.Generic;
using System.Security.Permissions;
using BicycleWorldCore;
using BicycleWorldEntityModel;
using BicycleWorldObjectModel;

namespace BicycleWorldServiceLibrary
{
    public partial class BicycleWorldService : IBicycleWorldService
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public Product GetProduct(int productID)
        {
            return ProductsCore.GetProduct(productID);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public List<ProductData> ProductList()
        {
            return ProductsCore.ProductList();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public int AddProduct(Product product)
        {
            return ProductsCore.AddProduct(product);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public bool RemoveProduct(int productID)
        {
            return ProductsCore.RemoveProduct(productID);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public Product UpdateProduct(Product product)
        {
            return ProductsCore.UpdateProduct(product);
        }
    }
}