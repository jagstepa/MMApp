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
        private int entityId;
        private int entityTypeId;
        private int entityRelationTypeId;
        private Country model;

        public CountryController(IMusicRepository db)
        {
            _db = db;
            entityTypeId = _db.GetEntityType<Country>();
            entityRelationTypeId = _db.GetEntityRelationType<Website>();
            model = new Country { Websites = new List<Relationship>() };
            entityId = 0;
            errorMessage = string.Empty;
        }

        public ActionResult Index()
        {
            return View(new List<Country>(_db.GetAll<Country>().Cast<Country>()));
        }

        [HttpGet]
        public ActionResult AddCountry()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

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
                Helper.DbAction<Country>(_db, "Add", country, out errorMessage, out entityId);

                if (country != null && errorMessage == string.Empty)
                {
                    foreach (Relationship relation in country.Websites)
                    {
                        relation.EntityId = entityId;
                        Helper.DbAction<Relationship>(_db, "Add", relation, out errorMessage, out entityId);
                    }

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("AddCountry");
        }

        public ActionResult UpdateCountry(int countryId)
        {
            Country entity = (Country)_db.Find<Country>(countryId);
            var list = _db.GetEntityRelationList<Website>(countryId, entityTypeId, entityRelationTypeId).ToList();

            if (list.Count > 0)
                entity.Websites = (List<Relationship>)list.Cast<Relationship>();
            else
                entity.Websites = new List<Relationship>();

            return View(entity);
        }

        [HttpPost]
        public ActionResult UpdateCountry(Country country)
        {
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
                AddCustomError<Country>(countryName, ErrorMessageType.Delete);
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
                AddCustomError<Website>(website.Url, ErrorMessageType.Duplicate);

            if (ModelState.IsValid)
            {
                int relationEntityId = _db.Add<Website>(website);

                Relationship relationship = new Relationship
                {
                    EntityTypeId = entityTypeId,
                    EntityId = website.EntityId,
                    EntityRelationTypeId = entityRelationTypeId,
                    EntityRelationId = relationEntityId,
                    EntityRelationValue = website.Url
                };

                model.Websites.Add(relationship);
                
                return RedirectToAction("AddCountry");
            }

            return RedirectToAction("AddWebsite");
        }

        private void CheckForErrors()
        {
            if (errorMessage != string.Empty)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }
        }

        private void AddCustomError<T>(string entityName, ErrorMessageType errorType) where T : IModelInterface
        {
            errorMessage = ErrorMessages.GetErrorMessage<T>(entityName, errorType);
            TempData["CustomError"] = errorMessage;
            ModelState.AddModelError("CustomError", errorMessage);
        }
    }
}
