using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;

namespace MMApp.Web.Controllers.Music
{
    public class CityController : Controller
    {
        private readonly IMusicRepository _dashboardSP = new MusicSPRepository();
        private Dictionary<string, string> paramDict = new Dictionary<string, string>();

        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }
            var model = new List<City>(_dashboardSP.GetAll<City>().Cast<City>());
            return View(new List<City>(_dashboardSP.GetAll<City>().Cast<City>()));
        }

        public ActionResult AddCity()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new City { Countries = new List<Country>(_dashboardSP.GetAll<Country>().Cast<Country>()) });
        }

        [HttpPost]
        public ActionResult AddCity(City city)
        {
            paramDict = Helpers.Helpers.GetDuplicateProperties<City>(city);

            if (_dashboardSP.CheckDuplicate<City>(paramDict))
            {
                var message = ErrorMessages.GetDuplicateErrorMessage<City>(city.CityName);
                TempData["CustomError"] = message;
                ModelState.AddModelError("CustomError", message);
            }

            if (ModelState.IsValid)
            {
                paramDict = Helpers.Helpers.GetEntityProperties<City>(city, false);

                _dashboardSP.Add<City>(paramDict);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddCity");
        }

        public ActionResult UpdateCity(int cityId)
        {
            var model = (City)_dashboardSP.Find<City>(cityId);
            model.Countries = new List<Country>(_dashboardSP.GetAll<Country>().Cast<Country>());

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateCity(City city)
        {
            var model = (City)_dashboardSP.Find<City>(city.Id);

            if (model.CityName == city.CityName && model.CountryId == city.CountryId && model.Website == city.Website)
            {
                TempData["CustomError"] = "City Name didn't change!";
                ModelState.AddModelError("CustomError", "City Name didn't change!");
            }

            var paramDict = new Dictionary<string, string>()
            {
            };

            if (_dashboardSP.CheckDuplicate<City>(paramDict))
            {
                TempData["CustomError"] = "Country ( " + city.CityName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Country ( " + city.CityName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Update(city);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateCity", "City", new { cityId = city.Id });
        }

        public ActionResult RemoveCity(int cityId, string cityName)
        {
            if (_dashboardSP.CheckDelete<City>(cityId))
            {
                TempData["CustomError"] = "Can't Delete. There are cities for Musician ( " + cityName + " )";
                ModelState.AddModelError("CustomError", "Can't Delete. There are cities for Musician ( " + cityName + " )");
            }
            else
            {
                _dashboardSP.Remove<City>(cityId);
            }

            return RedirectToAction("Index");
        }
    }
}
