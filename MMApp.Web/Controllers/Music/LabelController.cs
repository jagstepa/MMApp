using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class LabelController : Controller
    {
        IMusicRepository _db;
        private string errorMessage;

        public LabelController(IMusicRepository db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<Label>(_db.GetAll<Label>().Cast<Label>()));
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
            if (_db.CheckDuplicate<Label>(label))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(label.LabelName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Add<Label>(label);

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

            return View(_db.Find<Label>(labelId));
        }

        [HttpPost]
        public ActionResult UpdateLabel(Label label)
        {
            var model = (Label)_db.Find<Label>(label.Id);

            if (Helper.CheckForChanges<Country>(label, model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Label>(label.LabelName, ErrorMessageType.Changes);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Update<Label>(label);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateLabel", "Label", new { labelId = label.Id });
        }

        public ActionResult RemoveLabel(int labelId, string labelName)
        {
            var model = (Label)_db.Find<Label>(labelId);

            var result = _db.CheckDelete<Label>(model);

            if (result)
            {
                errorMessage = ErrorMessages.GetErrorMessage<Label>(labelName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _db.Remove<Label>(model);
            }

            return RedirectToAction("Index");
        }
    }
}
