using System;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Transactions;
using BicycleWorldCore;

namespace BicycleWorldServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,TransactionIsolationLevel = IsolationLevel.RepeatableRead)]
    [Serializable]
    [DurableService]
    public partial class BicycleWorldSalesService : IBicycleWorldSalesService
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public string Test() { return "Test successful."; }

        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public void Login() {}

        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public bool IsUserAdmin()
        {
            return CustomerCore.IsUserAdmin(CustomPrincipal.Current.Identity.Name);
        }
    }
}
