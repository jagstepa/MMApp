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
    public class CountryController : Controller
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

            return View(new List<Country>(_dashboardSP.GetAll<Country>().Cast<Country>()));
        }

        public ActionResult AddCountry()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new Country());
        }

        [HttpPost]
        public ActionResult AddCountry(Country country)
        {
            paramDict = Helpers.Helpers.GetDuplicateProperties<Country>(country);

            if (_dashboardSP.CheckDuplicate<Country>(paramDict))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(country.CountryName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                paramDict = Helpers.Helpers.GetEntityProperties<Country>(country, false);

                _dashboardSP.Add<Country>(paramDict);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddCountry");
        }

        public ActionResult UpdateCountry(int countryId)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(_dashboardSP.Find<Country>(countryId));
        }

        [HttpPost]
        public ActionResult UpdateCountry(Country country)
        {
            var model = (Country)_dashboardSP.Find<Country>(country.Id);

            if (Helpers.Helpers.CheckForChanges<Country>(country, model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(country.CountryName, ErrorMessageType.Changes);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Update<Country>(paramDict);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateCountry", "Country", new { countryId = country.Id });
        }

        public ActionResult RemoveCountry(int countryId, string countryName)
        {
            if (_dashboardSP.CheckDelete<Country>(countryId))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(countryName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _dashboardSP.Remove<Country>(countryId);
            }
            
            return RedirectToAction("Index");
        }
    }
}
