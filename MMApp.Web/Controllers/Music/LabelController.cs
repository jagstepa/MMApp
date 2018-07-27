using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class LabelController : Controller
    {
        private readonly IMusicRepository _dashboardSP = new MusicSPRepository();
        private Dictionary<string, string> paramDict = new Dictionary<string, string>();
        private string errorMessage;
        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<Label>(_dashboardSP.GetAll<Label>().Cast<Label>()));
        }

        public ActionResult AddLabel()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new Label());
        }

        [HttpPost]
        public ActionResult AddLabel(Label label)
        {
            paramDict.Add("LabelName", label.LabelName);

            if (_dashboardSP.CheckDuplicate<Label>(paramDict))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(label.LabelName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                paramDict.Add("Website", label.Website);

                _dashboardSP.Add<Label>(paramDict);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddLabel");
        }

        public ActionResult UpdateLabel(int labelId)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(_dashboardSP.Find<Label>(labelId));
        }

        [HttpPost]
        public ActionResult UpdateLabel(Label label)
        {
            var model = (Label)_dashboardSP.Find<Label>(label.Id);

            if (model.LabelName == label.LabelName && model.Website == label.Website)
            {
                TempData["CustomError"] = "Label Name didn't change!";
                ModelState.AddModelError("CustomError", "Label Name didn't change!");
            }

            var paramDict = new Dictionary<string, string>()
            {
            };

            if (_dashboardSP.CheckDuplicate<Label>(paramDict))
            {
                TempData["CustomError"] = "Label ( " + label.LabelName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Label ( " + label.LabelName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Update<Label>(paramDict);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateLabel", "Label", new { labelId = label.Id });
        }

        public ActionResult RemoveLabel(int labelId, string labelName)
        {
            var result = _dashboardSP.CheckDelete<Label>(labelId);

            if (result)
            {
                TempData["CustomError"] = "Can't Delete. There are labels for Musician ( " + labelName + " )";
                ModelState.AddModelError("CustomError", "Can't Delete. There are labels for Musician ( " + labelName + " )");
            }
            else
            {
                _dashboardSP.Remove<Label>(labelId);
            }

            return RedirectToAction("Index");
        }
    }
}
