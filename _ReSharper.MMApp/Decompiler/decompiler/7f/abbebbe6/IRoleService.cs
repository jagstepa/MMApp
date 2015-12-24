// Type: SecurityGuard.Interfaces.IRoleService
// Assembly: SecurityGuard, Version=1.0.53.580, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Stepa\Projects\MMApp\MMApp.Web\bin\SecurityGuard.dll

using System.Collections.Generic;
using System.Web.Security;

namespace SecurityGuard.Interfaces
{
  public interface IRoleService
  {
    string ApplicationName { get; set; }

    bool CacheRolesInCookie { get; }

    string CookieName { get; }

    string CookiePath { get; }

    CookieProtection CookieProtectionValue { get; }

    bool CookieRequireSSL { get; }

    bool CookieSlidingExpiration { get; }

    int CookieTimeout { get; }

    bool CreatePersistentCookie { get; }

    string Domain { get; }

    bool Enabled { get; }

    int MaxCachedResults { get; }

    RoleProvider Provider { get; }

    RoleProviderCollection Providers { get; }

    void AddUsersToRole(string[] usernames, string roleName);

    void AddUsersToRoles(string[] usernames, string[] roleNames);

    void AddUserToRole(string username, string roleName);

    void AddUserToRoles(string username, string[] roleNames);

    void CreateRole(string roleName);

    void DeleteCookie();

    bool DeleteRole(string roleName);

    bool DeleteRole(string roleName, bool throwOnPopulatedRole);

    string[] FindUsersInRole(string roleName, string usernameToMatch);

    string[] GetAllRoles();

    string[] GetRolesForUser();

    string[] GetRolesForUser(string username);

    string[] GetUsersInRole(string roleName);

    bool IsUserInRole(string roleName);

    bool IsUserInRole(string username, string roleName);

    void RemoveUserFromRole(string username, string roleName);

    void RemoveUserFromRoles(string username, string[] roleNames);

    void RemoveUsersFromRole(string[] usernames, string roleName);

    void RemoveUsersFromRoles(string[] usernames, string[] roleNames);

    bool RoleExists(string roleName);

    IEnumerable<string> AvailableRolesForUser(string userName);
  }
}
