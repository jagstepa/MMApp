using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;
using System;

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
            Country model;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            if (TempData["Country"] != null)
            {
                model = (Country)TempData["Country"];
            }
            else
            {
                model = new Country();
                model.Websites = new List<Relationship>();
                TempData["Country"] = model;
            }

            TempData.Keep();

            return View(model);
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
                int entityId = _db.Add<Country>(country);
                int entityTypeId = _db.GetEntityType<Country>();

                foreach (Relationship relation in country.Websites)
                {
                    relation.EntityId = entityId;
                    relation.EntityTypeId = entityTypeId;
                    relation.EntityRelationValue = "";
                }

                //_db.AddRelationship(country.Websites.ToList<IModelInterface>());

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

            IModelInterface entity = _db.Find<Country>(countryId);
            Country model = (Country)entity;
            List<Relationship> list = new List<Relationship>();
            //list.Add(new Website { Id = 1, Url = "http://something.com" });
            model.Websites = list;
            return View(model);
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

        public ActionResult RemoveWebsite(int websiteId)
        {
            var model = (Website)_db.Find<Website>(websiteId);

            _db.Remove<Website>(model);

            return RedirectToAction("Index");
        }

        public ActionResult ViewCountry(int countryId)
        {
            var model = (Country)_db.Find<Country>(countryId);
            model.IsReadOnly = true;
            List<Relationship> list = new List<Relationship>();
            //list.Add(new Website { Id = 1, Url = "http://something.com" });
            model.Websites = list;

            return View(model);
        }

        [HttpPost]
        public ActionResult Details(int countryId)
        {
            var model = (Country)_db.Find<Country>(countryId);
            model.IsReadOnly = true;
            List<Relationship> list = new List<Relationship>();
            //list.Add(new Website { Id = 1, Url = "http://something.com" });
            model.Websites = list;

            return PartialView(model);
        }

        public ActionResult Create()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(Country model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SaveChanges(model);
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }

            }
            //Something bad happened
            return PartialView("_Create", model);
        }

        static void SaveChanges(Country model)
        {
            // Uncommment next line to demonstrate errors in modal
            //throw new Exception("Error test");
        }

        public ActionResult AddWebsite()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new Website());
        }

        [HttpPost]
        public ActionResult AddWebsite(Website website)
        {
            if (_db.CheckDuplicate<Website>(website))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(website.Url, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                Country model;

                if (TempData["EntityTypeId"] == null)
                {
                    TempData["EntityTypeId"] = _db.GetEntityType<Country>();
                }

                if (TempData["EntityRelationTypeId"] == null)
                {
                    TempData["EntityRelationTypeId"] = _db.GetEntityRelationType<Website>();
                }

                int relationEntityId = _db.Add<Website>(website);

                Relationship relationship = new Relationship
                {
                    EntityTypeId = (int)TempData["EntityTypeId"],
                    EntityId = website.EntityId,
                    EntityRelationTypeId = (int)TempData["EntityTypeId"],
                    EntityRelationId = relationEntityId,
                    EntityRelationValue = website.Url
                };

                if (TempData["Country"] != null)
                {
                    model = (Country)TempData["Country"];
                    model.Websites.Add(relationship);
                    TempData["Country"] = model;
                }
                else
                {
                    model = new Country();
                    model.Websites = new List<Relationship>();
                    model.Websites.Add(relationship);
                    TempData["Country"] = model;
                }

                TempData.Keep();

                return RedirectToAction("AddCountry");
            }

            return RedirectToAction("AddWebsite");
        }
    }
}
