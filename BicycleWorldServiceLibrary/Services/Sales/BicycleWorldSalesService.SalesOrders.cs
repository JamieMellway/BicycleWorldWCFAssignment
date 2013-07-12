using System.Collections.Generic;
using System.Security.Permissions;
using System.ServiceModel;
using BicycleWorldCore;
using BicycleWorldEntityModel;
using BicycleWorldObjectModel;

namespace BicycleWorldServiceLibrary
{
    public partial class BicycleWorldSalesService : IBicycleWorldSalesService
    {
        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public int GetSalesOrderListCount()
        {
            return SalesOrderCore.GetSalesOrderListCount(CustomPrincipal.Current.Identity.Name);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public List<SalesOrder> GetSalesOrderList(int take, int skip)
        {
            return SalesOrderCore.GetSalesOrderList(CustomPrincipal.Current.Identity.Name, take, skip);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public List<SalesHistoryData> GetSalesHistory(int take, int skip)
        {
            return SalesOrderCore.GetSalesHistory(CustomPrincipal.Current.Identity.Name, take, skip);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = false)]
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public void SaveSalesOrder(List<ShoppingCartItem> shoppingCart)
        {
            SalesOrderCore.SaveSalesOrder(shoppingCart, CustomPrincipal.Current.Identity.Name);
        }
    }
}