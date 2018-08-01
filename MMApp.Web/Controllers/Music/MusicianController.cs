using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class MusicianController : Controller
    {
        IMusicRepository _db;
        private string errorMessage;

        public MusicianController(IMusicRepository db)
        {
            _db = db;
        }

        public ActionResult AddMusician()
        {
            var model = new Musician
                            {
                                Countries = new List<Country>(_db.GetAll<Country>().Cast<Country>()),
                                Cities = new List<City>(),
                                Genres = new List<Genre>(_db.GetAll<Genre>().Cast<Genre>()),
                                SelectedGenres = new List<Genre>(),
                                Occupations = new List<Occupation>(_db.GetAll<Occupation>().Cast<Occupation>()),
                                SelectedOccupations = new List<Occupation>(),
                                Instruments = new List<Instrument>(_db.GetAll<Instrument>().Cast<Instrument>()),
                                SelectedInstruments = new List<Instrument>(),
                                Labels = new List<Label>(_db.GetAll<Label>().Cast<Label>()),
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

            if (_db.CheckDuplicate<Musician>(musician))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(musician.StageName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Add<Musician>(musician);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddMusician");
        }

        public ActionResult UpdateMusician(int musicianId)
        {
            var model = (Musician)_db.Find<Musician>(musicianId);

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

            var model = (Musician)_db.Find<Musician>(musician.Id);

            if (Helper.CheckForChanges<Country>(musician, model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Musician>(musician.StageName, ErrorMessageType.Changes);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Update<Musician>(musician);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateMusician", "Musician", new { musicianId = musician.Id });
        }

        public ActionResult GetCities(int countryId)
        {
            var list = _db.GetAllForParent<City>(countryId, "Musician");
            List<City> targetList = new List<City>(list.Cast<City>());

            return Json(targetList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGenre(int genreId)
        {
            Genre genre = (Genre)_db.Find<Genre>(genreId);
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
            Genre genre = (Genre)_db.Find<Genre>(genreId);
            var list = (List<Genre>)TempData["SelectedGenres"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == genreId);
            list.Remove(itemToRemove);
            TempData["SelectedGenres"] = list;

            return Json(genre, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOccupation(int occupationId)
        {
            Occupation occupation = (Occupation)_db.Find<Occupation>(occupationId);
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
            Occupation occupation = (Occupation)_db.Find<Occupation>(occupationId);
            var list = (List<Occupation>)TempData["SelectedOccupations"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == occupationId);
            list.Remove(itemToRemove);
            TempData["SelectedOccupations"] = list;

            return Json(occupation, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInstrument(int instrumentId)
        {
            Instrument instrument = (Instrument)_db.Find<Instrument>(instrumentId);
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
            Instrument instrument = (Instrument)_db.Find<Instrument>(instrumentId);
            var list = (List<Instrument>)TempData["SelectedInstruments"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == instrumentId);
            list.Remove(itemToRemove);
            TempData["SelectedInstruments"] = list;

            return Json(instrument, JsonRequestBehavior.AllowGet);
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
    }
}
