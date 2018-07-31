using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using System;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class MusicianController : Controller
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

            return View(new List<Musician>(_dashboardSP.GetAll<Musician>().Cast<Musician>()));
        }

        public ActionResult AddMusician()
        {
            var model = new Musician
                            {
                                Countries = new List<Country>(_dashboardSP.GetAll<Country>().Cast<Country>()),
                                Cities = new List<City>(),
                                Genres = new List<Genre>(_dashboardSP.GetAll<Genre>().Cast<Genre>()),
                                SelectedGenres = new List<Genre>(),
                                Occupations = new List<Occupation>(_dashboardSP.GetAll<Occupation>().Cast<Occupation>()),
                                SelectedOccupations = new List<Occupation>(),
                                Instruments = new List<Instrument>(_dashboardSP.GetAll<Instrument>().Cast<Instrument>()),
                                SelectedInstruments = new List<Instrument>(),
                                Labels = new List<Label>(_dashboardSP.GetAll<Label>().Cast<Label>()),
                                SelectedLabels = new List<Label>()
                            };

            TempData["SelectedGenres"] = model.SelectedGenres;
            TempData["SelectedOccupations"] = model.SelectedOccupations;
            TempData["SelectedInstruments"] = model.SelectedInstruments;
            TempData["SelectedLabels"] = model.SelectedLabels;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddMusician(Musician musician)
        {
            musician.SelectedGenres = (List<Genre>)TempData["SelectedGenres"];
            musician.SelectedOccupations = (List<Occupation>)TempData["SelectedOccupations"];
            musician.SelectedInstruments = (List<Instrument>)TempData["SelectedInstruments"];
            musician.SelectedLabels = (List<Label>)TempData["SelectedLabels"];

            if (_dashboardSP.CheckDuplicate<Musician>(musician))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(musician.StageName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Add<Musician>(musician);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddMusician");
        }

        public ActionResult UpdateMusician(int musicianId)
        {
            var model = (Musician)_dashboardSP.Find<Musician>(musicianId);

            TempData["SelectedGenres"] = model.SelectedGenres;
            TempData["SelectedOccupations"] = model.SelectedOccupations;
            TempData["SelectedInstruments"] = model.SelectedInstruments;
            TempData["SelectedLabels"] = model.SelectedLabels;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateMusician(Musician musician)
        {
            musician.SelectedGenres = (List<Genre>)TempData["SelectedGenres"];
            musician.SelectedOccupations = (List<Occupation>)TempData["SelectedOccupations"];
            musician.SelectedInstruments = (List<Instrument>)TempData["SelectedInstruments"];
            musician.SelectedLabels = (List<Label>)TempData["SelectedLabels"];

            //var model = (Musician)_dashboard.Find<Musician>(musician.Id);

            //if (model.StageName == musician.StageName && model.Website == musician.Website)
            //{
            //    TempData["CustomError"] = "Musician Name didn't change!";
            //    ModelState.AddModelError("CustomError", "Musician Name didn't change!");
            //}

            //if (_dashboard.CheckDuplicate<Musician>(musician.StageName, musician.Website))
            //{
            //    TempData["CustomError"] = "Musician ( " + model.StageName + " ) already exists!";
            //    ModelState.AddModelError("CustomError", "Musician ( " + model.StageName + " ) already exists!");
            //}

            if (ModelState.IsValid)
            {
                _dashboardSP.Update<Musician>(musician);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateMusician", "Musician", new { musicianId = musician.Id });
        }

        public ActionResult GetCities(int countryId)
        {
            var list = _dashboardSP.GetAllForParent<City>(countryId, "Musician");
            List<City> targetList = new List<City>(list.Cast<City>());

            return Json(targetList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGenre(int genreId)
        {
            Genre genre = (Genre)_dashboardSP.Find<Genre>(genreId);
            var list = (List<Genre>)TempData["SelectedGenres"];
            var dulicateItem = list.SingleOrDefault(r => r.Id == genreId);
            if (dulicateItem != null )
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

        public ActionResult GetOccupation(int occupationId)
        {
            Occupation occupation = (Occupation)_dashboardSP.Find<Occupation>(occupationId);
            var list = (List<Occupation>)TempData["SelectedOccupations"];
            var dulicateItem = list.SingleOrDefault(r => r.Id == occupationId);
            if (dulicateItem != null)
            {
                TempData["SelectedOccupations"] = list;
                return null;
            }
            list.Add(occupation);
            TempData["SelectedOccupations"] = list;

            return Json(occupation, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveOccupation(int occupationId)
        {
            Occupation occupation = (Occupation)_dashboardSP.Find<Occupation>(occupationId);
            var list = (List<Occupation>)TempData["SelectedOccupations"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == occupationId);
            list.Remove(itemToRemove);
            TempData["SelectedOccupations"] = list;

            return Json(occupation, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInstrument(int instrumentId)
        {
            Instrument instrument = (Instrument)_dashboardSP.Find<Instrument>(instrumentId);
            var list = (List<Instrument>)TempData["SelectedInstruments"];
            var dulicateItem = list.SingleOrDefault(r => r.Id == instrumentId);
            if (dulicateItem != null)
            {
                TempData["SelectedInstruments"] = list;
                return null;
            }
            list.Add(instrument);
            TempData["SelectedInstruments"] = list;

            return Json(instrument, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveInstrument(int instrumentId)
        {
            Instrument instrument = (Instrument)_dashboardSP.Find<Instrument>(instrumentId);
            var list = (List<Instrument>)TempData["SelectedInstruments"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == instrumentId);
            list.Remove(itemToRemove);
            TempData["SelectedInstruments"] = list;

            return Json(instrument, JsonRequestBehavior.AllowGet);
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
    }
}
