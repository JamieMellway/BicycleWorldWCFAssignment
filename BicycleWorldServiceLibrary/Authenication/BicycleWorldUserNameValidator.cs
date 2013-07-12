using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using BicycleWorldCore;

namespace BicycleWorldServiceLibrary
{
    public class BicycleWorldUserNameValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName == null || password == null)
            {
                throw new ArgumentNullException();
            }

            if (!CustomerCore.Authenicate(userName, password))
            {
                throw new FaultException("Unknown username or incorrect password");
            }
        }
    }
}
