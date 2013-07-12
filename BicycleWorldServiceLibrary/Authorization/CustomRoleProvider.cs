using System;
using System.Linq;
using System.Web.Security;
using BicycleWorldCore;

namespace BicycleWorldServiceLibrary
{
    class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            return CustomerCore.GetUserRoles(username);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return GetRolesForUser(username).Contains(roleName);
        }

        #region Not Implemented
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
