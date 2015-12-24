using System.Web.Security;

namespace MMApp.Web.Areas.SecurityGuard.Models
{
    public class UserViewModel
    {
        public MembershipUser User { get; set; }
        public bool RequiresSecretQuestionAndAnswer { get; set; }
        public string[] Roles { get; set; }
        public string userName { get; set; }
    }
}
