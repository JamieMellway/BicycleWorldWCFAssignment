using System.Collections.Generic;
using System.Net.Security;
using System.ServiceModel;
using BicycleWorldObjectModel;

namespace BicycleWorldServiceLibrary
{
    public partial interface IBicycleWorldSalesService
    {
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract(Name = "AddItemToCart", IsInitiating = true, ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        bool AddItemToCart(string productNumber);

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract(Name = "RemoveItemFromCart", IsInitiating = false, ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        bool RemoveItemFromCart(string productNumber);

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract(Name = "GetShoppingCart", IsInitiating = false, ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        string GetShoppingCart();

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract(Name = "GetShoppingCartList", IsInitiating = true, ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        List<ShoppingCartItem> GetShoppingCartList();

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract(Name = "Checkout", IsInitiating = false, IsTerminating = true, ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        bool Checkout();
    }
}