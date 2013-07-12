using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Caching;

namespace BicycleWorldServiceLibrary
{
    class CustomPrincipal : IPrincipal
    {
        IIdentity _identity;
        string[] _roles;
        Cache _cache = HttpRuntime.Cache;
        object _loadLock = new object();

        public CustomPrincipal(IIdentity identity)
        {
            _identity = identity;
        }

        // helper method for easy access (without casting)
        public static CustomPrincipal Current
        {
            get
            {
                return Thread.CurrentPrincipal as CustomPrincipal;
            }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        // return all roles
        public string[] Roles
        {
            get
            {
                EnsureRoles();
                return _roles;
            }
        }

        // IPrincipal role check
        public bool IsInRole(string role)
        {
            EnsureRoles();

            return _roles.Contains(role);
        }

        // cache roles for subsequent requests
        protected virtual void EnsureRoles()
        {
            string cacheKey = "roles_" + _identity.Name;

            if (_roles == null)
            {
                lock (_loadLock)
                {
                    if (_roles == null)
                    {
                        if (_cache[cacheKey] == null)
                        {
                            _roles = System.Web.Security.Roles.GetRolesForUser(_identity.Name);
                            _cache.Add(cacheKey,
                                _roles,
                                null,
                                DateTime.Now.AddMinutes(30),
                                Cache.NoSlidingExpiration,
                                CacheItemPriority.Normal,
                                null);
                        }
                        else
                        {
                            _roles = (string[])_cache[cacheKey];
                        }
                    }
                }
            }
        }
    }
}
