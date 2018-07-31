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
    public class BandController : Controller
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

            return View(new List<Band>(_dashboardSP.GetAll<Band>().Cast<Band>()));
        }

        public ActionResult AddBand()
        {
            var model = new Band
            {
                Countries = new List<Country>(_dashboardSP.GetAll<Country>().Cast<Country>()),
                Cities = new List<City>(),
                Genres = new List<Genre>(_dashboardSP.GetAll<Genre>().Cast<Genre>()),
                SelectedGenres = new List<Genre>(),
                Labels = new List<Label>(_dashboardSP.GetAll<Label>().Cast<Label>()),
                SelectedLabels = new List<Label>(),
                Musicians = new List<Musician>(_dashboardSP.GetAll<Musician>().Cast<Musician>()),
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

            if (_dashboardSP.CheckDuplicate<Musician>(band))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(band.BandName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Add<Musician>(band);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddBand");
        }

        public ActionResult UpdateBand(int bandId)
        {
            var model = (Band)_dashboardSP.Find<Band>(bandId);

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
            band.SelectedGenres = (List<Genre>)TempData["SelectedGenres"];
            band.SelectedLabels = (List<Label>)TempData["SelectedLabels"];
            band.SelectedMusicians = (List<Musician>)TempData["SelectedMusicians"];
            band.MusicianActivity = (List<MusicianActivity>)TempData["MusicianActivity"];

            if (ModelState.IsValid)
            {
                _dashboardSP.Update<Band>(band);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateBand", "Band", new { bandId = band.Id });
        }

        public ActionResult RemoveBand(int bandId, string bandName)
        {
            var model = (Band)_dashboardSP.Find<Band>(bandId);

            if (_dashboardSP.CheckDelete<Band>(model))
            {
                TempData["CustomError"] = "Can't Delete. There are musicians for Band ( " + bandName + " )";
                ModelState.AddModelError("CustomError", "Can't Delete. There are musicians for Band ( " + bandName + " )");
            }
            else
            {
                _dashboardSP.Remove<Band>(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult GetCities(int countryId)
        {
            var list = _dashboardSP.GetAllForParent<City>(countryId, "Band");
            List<City> targetList = new List<City>(list.Cast<City>());

            return Json(targetList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGenre(int genreId)
        {
            Genre genre = (Genre)_dashboardSP.Find<Genre>(genreId);
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
            Genre genre = (Genre)_dashboardSP.Find<Genre>(genreId);
            var list = (List<Genre>)TempData["SelectedGenres"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == genreId);
            list.Remove(itemToRemove);
            TempData["SelectedGenres"] = list;

            return Json(genre, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLabel(int labelId)
        {
            Label label = (Label)_dashboardSP.Find<Label>(labelId);
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
            Label label = (Label)_dashboardSP.Find<Label>(labelId);
            var list = (List<Label>)TempData["SelectedLabels"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == labelId);
            list.Remove(itemToRemove);
            TempData["SelectedLabels"] = list;

            return Json(label, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMusician(int musicianId)
        {
            Musician musician = (Musician)_dashboardSP.Find<Musician>(musicianId);
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
            Musician musician = (Musician)_dashboardSP.Find<Musician>(musicianId);
            var list = (List<Musician>)TempData["SelectedMusicians"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == musicianId);
            list.Remove(itemToRemove);
            TempData["SelectedMusicians"] = list;

            return Json(musician, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetActivity(int musicianId, string yearFrom, string yearTo, int bandId)
        {
            Musician musician = (Musician)_dashboardSP.Find<Musician>(musicianId);
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
            Musician musician = (Musician)_dashboardSP.Find<Musician>(musicianId);
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
