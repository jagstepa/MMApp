using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class BandController : Controller
    {
        IMusicRepository _db;
        private string errorMessage;

        public BandController(IMusicRepository db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<Band>(_db.GetAll<Band>().Cast<Band>()));
        }

        public ActionResult AddBand()
        {
            var model = new Band
            {
                Countries = new List<Country>(_db.GetAll<Country>().Cast<Country>()),
                Cities = new List<City>(),
                Genres = new List<Genre>(_db.GetAll<Genre>().Cast<Genre>()),
                SelectedGenres = new List<Genre>(),
                Labels = new List<Label>(_db.GetAll<Label>().Cast<Label>()),
                SelectedLabels = new List<Label>(),
                Musicians = new List<Musician>(_db.GetAll<Musician>().Cast<Musician>()),
                SelectedMusicians = new List<Musician>(),
                MusicianActivity = new List<MusicianActivity>()
            };

            TempData["SelectedGenres"] = model.SelectedGenres;
            TempData["SelectedLabels"] = model.SelectedLabels;
            TempData["SelectedMusicians"] = model.SelectedMusicians;
            TempData["MusicianActivity"] = model.MusicianActivity;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddBand(Band band)
        {
            band.SelectedGenres = (List<Genre>)TempData["SelectedGenres"];
            band.SelectedLabels = (List<Label>)TempData["SelectedLabels"];
            band.SelectedMusicians = (List<Musician>)TempData["SelectedMusicians"];
            band.MusicianActivity = (List<MusicianActivity>)TempData["MusicianActivity"];

            if (_db.CheckDuplicate<Musician>(band))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(band.BandName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Add<Musician>(band);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddBand");
        }

        public ActionResult UpdateBand(int bandId)
        {
            var model = (Band)_db.Find<Band>(bandId);

            TempData["SelectedGenres"] = model.SelectedGenres;
            TempData["SelectedLabels"] = model.SelectedLabels;
            TempData["SelectedMusicians"] = model.SelectedMusicians;
            TempData["MusicianActivity"] = model.MusicianActivity;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateBand(Band band)
        {
            var model = (Country)_db.Find<Country>(band.Id);

            band.SelectedGenres = (List<Genre>)TempData["SelectedGenres"];
            band.SelectedLabels = (List<Label>)TempData["SelectedLabels"];
            band.SelectedMusicians = (List<Musician>)TempData["SelectedMusicians"];
            band.MusicianActivity = (List<MusicianActivity>)TempData["MusicianActivity"];

            if (Helper.CheckForChanges<Band>(band, model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(band.BandName, ErrorMessageType.Changes);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Update<Band>(band);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateBand", "Band", new { bandId = band.Id });
        }

        public ActionResult RemoveBand(int bandId, string bandName)
        {
            var model = (Band)_db.Find<Band>(bandId);

            if (_db.CheckDelete<Band>(model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(bandName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _db.Remove<Band>(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult GetCities(int countryId)
        {
            var list = _db.GetAllForParent<City>(countryId, "Band");
            List<City> targetList = new List<City>(list.Cast<City>());

            return Json(targetList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGenre(int genreId)
        {
            Genre genre = (Genre)_db.Find<Genre>(genreId);
            var list = (List<Genre>)TempData["SelectedGenres"];
            var dulicateItem = list.SingleOrDefault(r => r.Id == genreId);
            if (dulicateItem != null)
            {
                TempData["SelectedGenres"] = list;
                return null;
            }
            list.Add(genre);
            TempData["SelectedGenres"] = list;

            return Json(genre, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveGenre(int genreId)
        {
            Genre genre = (Genre)_db.Find<Genre>(genreId);
            var list = (List<Genre>)TempData["SelectedGenres"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == genreId);
            list.Remove(itemToRemove);
            TempData["SelectedGenres"] = list;

            return Json(genre, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLabel(int labelId)
        {
            Label label = (Label)_db.Find<Label>(labelId);
            var list = (List<Label>)TempData["SelectedLabels"];
            var dulicateItem = list.SingleOrDefault(r => r.Id == labelId);
            if (dulicateItem != null)
            {
                TempData["SelectedLabels"] = list;
                return null;
            }
            list.Add(label);
            TempData["SelectedLabels"] = list;

            return Json(label, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveLabel(int labelId)
        {
            Label label = (Label)_db.Find<Label>(labelId);
            var list = (List<Label>)TempData["SelectedLabels"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == labelId);
            list.Remove(itemToRemove);
            TempData["SelectedLabels"] = list;

            return Json(label, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMusician(int musicianId)
        {
            Musician musician = (Musician)_db.Find<Musician>(musicianId);
            var list = (List<Musician>)TempData["SelectedMusicians"];
            var duplicateItem = list.SingleOrDefault(r => r.Id == musicianId);
            if (duplicateItem != null)
            {
                TempData["SelectedMusicians"] = list;
                return null;
            }
            list.Add(musician);
            TempData["SelectedMusicians"] = list;

            return Json(musician, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveMusician(int musicianId)
        {
            Musician musician = (Musician)_db.Find<Musician>(musicianId);
            var list = (List<Musician>)TempData["SelectedMusicians"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == musicianId);
            list.Remove(itemToRemove);
            TempData["SelectedMusicians"] = list;

            return Json(musician, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetActivity(int musicianId, string yearFrom, string yearTo, int bandId)
        {
            Musician musician = (Musician)_db.Find<Musician>(musicianId);
            var activity = musician.StageName + " (" + yearFrom + " - " + yearTo + ")";
            var list = (List<MusicianActivity>)TempData["MusicianActivity"];

            MusicianActivity musicianActivity = new MusicianActivity
                                                    {
                                                        Activity = activity, 
                                                        MusicianId = musicianId,
                                                        YearFrom = yearFrom,
                                                        YearTo = yearTo,
                                                        MusicianStageName = musician.StageName,
                                                        BandId = bandId
                                                    };

            var duplicateItem = list.SingleOrDefault(r => r.Activity == activity);
            if (duplicateItem != null)
            { musicianActivity.Activity = ""; }
            else
            { list.Add(musicianActivity); }

            TempData["MusicianActivity"] = list;

            return Json(musicianActivity, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveActivity(int musicianId, string activity)
        {
            Musician musician = (Musician)_db.Find<Musician>(musicianId);
            //activity = musician.StageName + " (" + activity + ")";
            //musician.MusicianActivity = activity;
            var list = (List<MusicianActivity>)TempData["MusicianActivity"];
            var itemToRemove = list.SingleOrDefault(r => r.Activity == activity);
            list.Remove(itemToRemove);
            TempData["MusicianActivity"] = list;

            return Json(musician, JsonRequestBehavior.AllowGet);
        }
    }
}
