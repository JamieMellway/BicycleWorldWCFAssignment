using System.ServiceModel;
using BicycleWorldObjectModel;

namespace BicycleWorldServiceLibrary
{
    [ServiceContract(
        Namespace = "http://BicycleWorld.ca/2013/03/12",
        Name = "BicycleWorldService")]
    public partial interface IBicycleWorldService
    {
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        string Test();

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        void Login();
    }
}