using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class OccupationController : Controller
    {
        IMusicRepository _db;
        private string errorMessage;

        public OccupationController(IMusicRepository db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<Occupation>(_db.GetAll<Occupation>().Cast<Occupation>()));
        }

        public ActionResult AddOccupation()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new Occupation());
        }

        [HttpPost]
        public ActionResult AddOccupation(Occupation occupation)
        {
            if (_db.CheckDuplicate<Occupation>(occupation))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(occupation.OccupationName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Add<Occupation>(occupation);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddOccupation");
        }

        public ActionResult UpdateOccupation(int occupationId)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(_db.Find<Occupation>(occupationId));
        }

        [HttpPost]
        public ActionResult UpdateOccupation(Occupation occupation)
        {
            var model = (Occupation)_db.Find<Occupation>(occupation.Id);

            if (Helper.CheckForChanges<Occupation>(occupation, model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(occupation.OccupationName, ErrorMessageType.Changes);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Update<Occupation>(occupation);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateOccupation", "Occupation", new { occupationId = occupation.Id });
        }

        public ActionResult RemoveOccupation(int occupationId, string occupationName)
        {
            var model = (Occupation)_db.Find<Occupation>(occupationId);

            if (_db.CheckDelete<Occupation>(model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Occupation>(occupationName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _db.Remove<Occupation>(model);
            }

            return RedirectToAction("Index");
        }
    }
}
