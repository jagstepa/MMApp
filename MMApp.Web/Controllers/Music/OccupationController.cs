using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;

namespace MMApp.Web.Controllers.Music
{
    public class OccupationController : Controller
    {
        private readonly IMusicRepository _dashboardSP = new MusicSPRepository();
        private Dictionary<string, string> paramDict = new Dictionary<string, string>();

        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<Occupation>(_dashboardSP.GetAll<Occupation>().Cast<Occupation>()));
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
            paramDict.Add("OccupationName", occupation.OccupationName);

            if (_dashboardSP.CheckDuplicate<Occupation>(paramDict))
            {
                TempData["CustomError"] = "Occupation ( " + occupation.OccupationName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Occupation ( " + occupation.OccupationName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                paramDict.Add("Website", occupation.Website);

                _dashboardSP.Add<Occupation>(paramDict);

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

            return View(_dashboardSP.Find<Occupation>(occupationId));
        }

        [HttpPost]
        public ActionResult UpdateOccupation(Occupation occupation)
        {
            var model = (Occupation)_dashboardSP.Find<Occupation>(occupation.Id);

            if (model.OccupationName == occupation.OccupationName && model.Website == occupation.Website)
            {
                TempData["CustomError"] = "Occupation Name didn't change!";
                ModelState.AddModelError("CustomError", "Occupation Name didn't change!");
            }

            var paramDict = new Dictionary<string, string>()
            {
            };

            if (_dashboardSP.CheckDuplicate<Occupation>(paramDict))
            {
                TempData["CustomError"] = "Occupation ( " + occupation.OccupationName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Occupation ( " + occupation.OccupationName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Update(occupation);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateOccupation", "Occupation", new { occupationId = occupation.Id });
        }

        public ActionResult RemoveOccupation(int occupationId, string occupationName)
        {
            if (_dashboardSP.CheckDelete<Occupation>(occupationId))
            {
                TempData["CustomError"] = "Can't Delete. There are occupations for Musician ( " + occupationName + " )";
                ModelState.AddModelError("CustomError", "Can't Delete. There are occupations for Musician ( " + occupationName + " )");
            }
            else
            {
                _dashboardSP.Remove<Occupation>(occupationId);
            }

            return RedirectToAction("Index");
        }
    }
}
