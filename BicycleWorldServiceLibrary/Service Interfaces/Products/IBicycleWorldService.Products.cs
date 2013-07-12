using System.Collections.Generic;
using System.ServiceModel;
using BicycleWorldEntityModel;
using BicycleWorldObjectModel;

namespace BicycleWorldServiceLibrary
{

    public partial interface IBicycleWorldService
    {
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        Product GetProduct(int productID);

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        List<ProductData> ProductList();

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        int AddProduct(Product product);

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        bool RemoveProduct(int productID);

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        Product UpdateProduct(Product product);
    }
}