using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;

namespace BicycleWorldServiceLibrary
{
    class AuthorizationPolicy : IAuthorizationPolicy
    {
        Guid _id = Guid.NewGuid();

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            IIdentity client = GetClientIdentity(evaluationContext);

            evaluationContext.Properties["Principal"] = new CustomPrincipal(client);

            return true;
        }

        private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                throw new Exception("No Identity found");

            IList<IIdentity> identities = obj as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
                throw new Exception("No Identity found");

            return identities[0];
        }

        public System.IdentityModel.Claims.ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        public string Id
        {
            get { return _id.ToString(); }
        }
    }
}
