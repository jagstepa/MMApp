// Type: SecurityGuard.Interfaces.IMembershipService
// Assembly: SecurityGuard, Version=1.0.53.580, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Stepa\Projects\MMApp\MMApp.Web\bin\SecurityGuard.dll

using System.Web.Security;

namespace SecurityGuard.Interfaces
{
  public interface IMembershipService
  {
    string ApplicationName { get; set; }

    bool EnablePasswordReset { get; }

    bool EnablePasswordRetrieval { get; }

    string HashAlgorithmType { get; }

    int MaxInvalidPasswordAttempts { get; }

    int MinRequiredNonAlphanumericCharacters { get; }

    int MinRequiredPasswordLength { get; }

    int PasswordAttemptWindow { get; }

    string PasswordStrengthRegularExpression { get; }

    MembershipProvider Provider { get; }

    MembershipProviderCollection Providers { get; }

    bool RequiresQuestionAndAnswer { get; }

    int UserIsOnlineTimeWindow { get; }

    event MembershipValidatePasswordEventHandler ValidatingPassword;

    MembershipUser CreateUser(string username, string password);

    MembershipUser CreateUser(string username, string password, string email);

    MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, out MembershipCreateStatus status);

    MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status);

    bool DeleteUser(string username);

    bool DeleteUser(string username, bool deleteAllRelatedData);

    MembershipUserCollection FindUsersByEmail(string emailToMatch);

    MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords);

    MembershipUserCollection FindUsersByName(string usernameToMatch);

    MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);

    string GeneratePassword(int length, int numberOfNonAlphanumericCharacters);

    MembershipUserCollection GetAllUsers();

    MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords);

    int GetNumberOfUsersOnline();

    MembershipUser GetUser();

    MembershipUser GetUser(bool userIsOnline);

    MembershipUser GetUser(object providerUserKey);

    MembershipUser GetUser(string username);

    MembershipUser GetUser(object providerUserKey, bool userIsOnline);

    MembershipUser GetUser(string username, bool userIsOnline);

    string GetUserNameByEmail(string emailToMatch);

    void UpdateUser(MembershipUser user);

    bool ValidateUser(string username, string password);
  }
}
