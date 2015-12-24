using Mvc.Mailer;
using MMApp.Web.Mailers.Models;

namespace MMApp.Web.Mailers
{ 
    public interface IPasswordResetMailer
    {
			MvcMailMessage PasswordReset(MailerModel model);
	}
}