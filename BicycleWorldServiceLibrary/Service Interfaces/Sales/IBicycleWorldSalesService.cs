using System.Net.Security;
using System.ServiceModel;
using BicycleWorldObjectModel;

namespace BicycleWorldServiceLibrary
{
    [ServiceContract(
        SessionMode = SessionMode.Required,
        ProtectionLevel = ProtectionLevel.EncryptAndSign,
        Namespace = "http://BicycleWorldSales.ca/2013/03/13",
        Name = "BicycleWorldSalesService")]
    public partial interface IBicycleWorldSalesService
    {
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        string Test();

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        void Login();

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        bool IsUserAdmin();
    }
}