using System.Security.Permissions;
using System.ServiceModel;

namespace BicycleWorldServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public partial class BicycleWorldService : IBicycleWorldService
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public void Login() { }

        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public string Test() { return "Test successful."; }
    }
}
