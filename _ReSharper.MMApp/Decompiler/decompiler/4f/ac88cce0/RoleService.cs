// Type: SecurityGuard.Services.RoleService
// Assembly: SecurityGuard, Version=1.0.53.580, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Stepa\Projects\MMApp\MMApp.Web\bin\SecurityGuard.dll

using SecurityGuard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace SecurityGuard.Services
{
  public class RoleService : IRoleService
  {
    private readonly RoleProvider roleProvider;

    public string ApplicationName
    {
      get
      {
        return this.roleProvider.ApplicationName;
      }
      set
      {
        this.roleProvider.ApplicationName = value;
      }
    }

    public bool CacheRolesInCookie
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public string CookieName
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public string CookiePath
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public CookieProtection CookieProtectionValue
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public bool CookieRequireSSL
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public bool CookieSlidingExpiration
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public int CookieTimeout
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public bool CreatePersistentCookie
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public string Domain
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public bool Enabled
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public int MaxCachedResults
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public RoleProvider Provider
    {
      get
      {
        return this.roleProvider;
      }
    }

    public RoleProviderCollection Providers
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public RoleService(RoleProvider roleProvider)
    {
      this.roleProvider = roleProvider;
    }

    public void AddUsersToRole(string[] usernames, string roleName)
    {
      this.roleProvider.AddUsersToRoles(usernames, new string[1]
      {
        roleName
      });
    }

    public void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
      this.roleProvider.AddUsersToRoles(usernames, roleNames);
    }

    public void AddUserToRole(string username, string roleName)
    {
      this.roleProvider.AddUsersToRoles(new string[1]
      {
        username
      }, new string[1]
      {
        roleName
      });
    }

    public void AddUserToRoles(string username, string[] roleNames)
    {
      this.roleProvider.AddUsersToRoles(new string[1]
      {
        username
      }, roleNames);
    }

    public void CreateRole(string roleName)
    {
      this.roleProvider.CreateRole(roleName);
    }

    public void DeleteCookie()
    {
      throw new NotImplementedException();
    }

    public bool DeleteRole(string roleName)
    {
      return this.roleProvider.DeleteRole(roleName, true);
    }

    public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    {
      return this.roleProvider.DeleteRole(roleName, throwOnPopulatedRole);
    }

    public string[] FindUsersInRole(string roleName, string usernameToMatch)
    {
      return this.roleProvider.FindUsersInRole(roleName, usernameToMatch);
    }

    public string[] GetAllRoles()
    {
      return this.roleProvider.GetAllRoles();
    }

    public string[] GetRolesForUser()
    {
      return this.roleProvider.GetRolesForUser((string) null);
    }

    public string[] GetRolesForUser(string username)
    {
      return this.roleProvider.GetRolesForUser(username);
    }

    public string[] GetUsersInRole(string roleName)
    {
      return this.roleProvider.GetUsersInRole(roleName);
    }

    public bool IsUserInRole(string roleName)
    {
      return this.roleProvider.IsUserInRole((string) null, roleName);
    }

    public bool IsUserInRole(string username, string roleName)
    {
      return this.roleProvider.IsUserInRole(username, roleName);
    }

    public void RemoveUserFromRole(string username, string roleName)
    {
      this.roleProvider.RemoveUsersFromRoles(new string[1]
      {
        username
      }, new string[1]
      {
        roleName
      });
    }

    public void RemoveUserFromRoles(string username, string[] roleNames)
    {
      this.roleProvider.RemoveUsersFromRoles(new string[1]
      {
        username
      }, roleNames);
    }

    public void RemoveUsersFromRole(string[] usernames, string roleName)
    {
      this.roleProvider.RemoveUsersFromRoles(usernames, new string[1]
      {
        roleName
      });
    }

    public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
      this.roleProvider.RemoveUsersFromRoles(usernames, roleNames);
    }

    public bool RoleExists(string roleName)
    {
      return this.roleProvider.RoleExists(roleName);
    }

    public IEnumerable<string> AvailableRolesForUser(string userName)
    {
      return Enumerable.Except<string>((IEnumerable<string>) this.roleProvider.GetAllRoles(), (IEnumerable<string>) this.roleProvider.GetRolesForUser(userName));
    }
  }
}
