using System.Collections.Generic;
using System.Net.Security;
using System.ServiceModel;
using BicycleWorldEntityModel;
using BicycleWorldObjectModel;


namespace BicycleWorldServiceLibrary
{
    public partial interface IBicycleWorldSalesService
    {
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        int GetSalesOrderListCount();

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract(ProtectionLevel=ProtectionLevel.EncryptAndSign)]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        List<SalesOrder> GetSalesOrderList(int take, int skip);

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        List<SalesHistoryData> GetSalesHistory(int take, int skip);
    }
}