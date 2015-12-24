using System.Web.Mvc;

namespace MMApp.Web.Areas.SecurityGuard.Models
{
    public class ManageRolesViewModel
    {
        public SelectList Roles { get; set; }
        public string[] RoleList { get; set; }
    }
}
