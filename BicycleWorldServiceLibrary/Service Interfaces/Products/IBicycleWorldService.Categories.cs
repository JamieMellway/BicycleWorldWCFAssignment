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
        List<ProductCategory> CategoriesList();

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        ProductCategory GetCategory(int categoryID);

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        int AddCategory(ProductCategory category);

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        bool RemoveCategory(int categoryID);

        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DatabaseFault))]
        [OperationContract]
        ProductCategory UpdateCategory(ProductCategory category);
    }
}