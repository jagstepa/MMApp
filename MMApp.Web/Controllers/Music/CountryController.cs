using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class CountryController : Controller
    {
        IMusicRepository _db;
        private string errorMessage;

        public CountryController(IMusicRepository db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<Country>(_db.GetAll<Country>().Cast<Country>()));
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
            if (_db.CheckDuplicate<Country>(country))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(country.CountryName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Add<Country>(country);

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

            return View(_db.Find<Country>(countryId));
        }

        [HttpPost]
        public ActionResult UpdateCountry(Country country)
        {
            var model = (Country)_db.Find<Country>(country.Id);

            if (Helper.CheckForChanges<Country>(country, model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(country.CountryName, ErrorMessageType.Changes);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Update<Country>(country);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateCountry", "Country", new { countryId = country.Id });
        }

        public ActionResult RemoveCountry(int countryId, string countryName)
        {
            var model = (Country)_db.Find<Country>(countryId);

            if (_db.CheckDelete<Country>(model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(countryName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _db.Remove<Country>(model);
            }
            
            return RedirectToAction("Index");
        }
    }
}
