using System.Collections.Generic;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Description;
using BicycleWorldCore;
using BicycleWorldObjectModel;

namespace BicycleWorldServiceLibrary
{
    public partial class BicycleWorldSalesService : IBicycleWorldSalesService
    {
        [OperationBehavior]
        [DurableOperation(CanCreateInstance = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public bool AddItemToCart(string productNumber)
        {
            bool result = ShoppingCartCore.Current.AddItemToCart(productNumber);
            return result;
        }

        [OperationBehavior]
        [DurableOperation(CanCreateInstance = false)]
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public bool RemoveItemFromCart(string productNumber)
        {
            return ShoppingCartCore.Current.RemoveItemFromCart(productNumber);
        }

        [OperationBehavior]
        [DurableOperation(CanCreateInstance = false)]
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public string GetShoppingCart()
        {
            return ShoppingCartCore.Current.GetShoppingCart();
        }

        [OperationBehavior]
        [DurableOperation(CanCreateInstance = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public List<ShoppingCartItem> GetShoppingCartList()
        {
            return ShoppingCartCore.Current.GetShoppingCartList();
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = false)]
        [DurableOperation(CanCreateInstance = false, CompletesInstance=true)]
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public bool Checkout()
        {
            return ShoppingCartCore.Current.Checkout();
        }
    }
}
