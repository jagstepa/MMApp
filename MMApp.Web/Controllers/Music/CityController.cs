﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class CityController : Controller
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
            paramDict = Helper.GetEntityProperties<City>(city, false);

            if (_dashboardSP.CheckDuplicate<City>(city))
            {
                errorMessage = ErrorMessages.GetErrorMessage<City>(city.CityName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Add<City>(city);

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

            if (_dashboardSP.CheckDuplicate<City>(city))
            {
                TempData["CustomError"] = "Country ( " + city.CityName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Country ( " + city.CityName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Update<City>(city);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateCity", "City", new { cityId = city.Id });
        }

        public ActionResult RemoveCity(int cityId, string cityName)
        {
            var model = (City)_dashboardSP.Find<City>(cityId);

            if (_dashboardSP.CheckDelete<City>(model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<City>(cityName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _dashboardSP.Remove<City>(model);
            }

            return RedirectToAction("Index");
        }
    }
}
