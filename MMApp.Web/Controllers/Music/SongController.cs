using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class SongController : Controller
    {
        IMusicRepository _db;
        private string errorMessage;

        public SongController(IMusicRepository db)
        {
            _db = db;
        }

        public ActionResult Index(string searchText)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            List<Song> targetList = new List<Song>();
            if (!string.IsNullOrEmpty(searchText))
            {
                var list = _db.GetAllForText<Song>(searchText);
                targetList = new List<Song>(list.Cast<Song>());
            }

            return View(targetList);
        }

        public ActionResult AddSong()
        {
            var model = new Song
            {
                Musicians = new List<Musician>(_db.GetAll<Musician>().Cast<Musician>()),
                SelectedMusicians = new List<Musician>()
            };

            TempData["SelectedMusicians"] = model.SelectedMusicians;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddSong(Song song)
        {
            song.SelectedMusicians = (List<Musician>)TempData["SelectedMusicians"];

            if (_db.CheckDuplicate<Song>(song))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(song.SongName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Add<Song>(song);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddSong");
        }

        public ActionResult UpdateSong(int songId)
        {
            var model = (Song)_db.Find<Song>(songId);

            TempData["SelectedMusicians"] = model.SelectedMusicians;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateSong(Song song)
        {
            song.SelectedMusicians = (List<Musician>)TempData["SelectedMusicians"];

            var model = (Song)_db.Find<Song>(song.Id);

            if (Helper.CheckForChanges<Song>(song, model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Song>(song.SongName, ErrorMessageType.Changes);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Update<Song>(song);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateSong", "Song", new { songId = song.Id });
        }

        public ActionResult RemoveSong(int songId, string songName)
        {
            var model = (Song)_db.Find<Song>(songId);

            if (_db.CheckDelete<Song>(model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(songName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _db.Remove<Song>(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult SearchSongs(string searchText)
        {
            return RedirectToAction("Index", "Song", new {searchText });
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
    }
}
