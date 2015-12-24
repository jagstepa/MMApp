using System.Web.Mvc;
using System.Web.Security;
using MMApp.Web.Areas.SecurityGuard.Models;
using SecurityGuard.Services;
using SecurityGuard.Interfaces;
using MMApp.Web.Controllers;

namespace MMApp.Web.Areas.SecurityGuard.Controllers
{
    [Authorize(Roles = "SecurityGuard")]
    public class DashboardController : BaseController
    {

        #region ctors

        private readonly IMembershipService membershipService;
        private readonly IRoleService roleService;

        public DashboardController()
        {
            roleService = new RoleService(Roles.Provider);
            membershipService = new MembershipService(Membership.Provider);
        }

        #endregion


        public virtual ActionResult Index()
        {
            DashboardViewModel viewModel = new DashboardViewModel();
            int totalRecords;

            membershipService.GetAllUsers(0, 20, out totalRecords);
            viewModel.TotalUserCount = totalRecords.ToString();
            viewModel.TotalUsersOnlineCount = membershipService.GetNumberOfUsersOnline().ToString();
            viewModel.TotalRolesCount = roleService.GetAllRoles().Length.ToString();

            return View(viewModel);
        }

    }
}
