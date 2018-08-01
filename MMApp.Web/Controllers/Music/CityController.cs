using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class CityController : Controller
    {
        IMusicRepository _db;
        private string errorMessage;

        public CityController(IMusicRepository db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<City>(_db.GetAll<City>().Cast<City>()));
        }

        public ActionResult AddCity()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new City { Countries = new List<Country>(_db.GetAll<Country>().Cast<Country>()) });
        }

        [HttpPost]
        public ActionResult AddCity(City city)
        {
            if (_db.CheckDuplicate<City>(city))
            {
                errorMessage = ErrorMessages.GetErrorMessage<City>(city.CityName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Add<City>(city);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddCity");
        }

        public ActionResult UpdateCity(int cityId)
        {
            var model = (City)_db.Find<City>(cityId);
            model.Countries = new List<Country>(_db.GetAll<Country>().Cast<Country>());

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateCity(City city)
        {
            var model = (City)_db.Find<City>(city.Id);

            if (_db.CheckDuplicate<City>(city))
            {
                errorMessage = ErrorMessages.GetErrorMessage<City>(city.CityName, ErrorMessageType.Changes);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Update<City>(city);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateCity", "City", new { cityId = city.Id });
        }

        public ActionResult RemoveCity(int cityId, string cityName)
        {
            var model = (City)_db.Find<City>(cityId);

            if (_db.CheckDelete<City>(model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<City>(cityName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _db.Remove<City>(model);
            }

            return RedirectToAction("Index");
        }
    }
}
