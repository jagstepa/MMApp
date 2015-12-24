using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;

namespace MMApp.Web.Controllers.Music
{
    public class AlbumController : Controller
    {
        private readonly IMusicRepository _dashboardSP = new MusicSPRepository();

        public ActionResult Index(int bandId, string bandName)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            ViewBag.BandName = bandName;
            ViewBag.BandId = bandId;
            return View(new List<Album>(_dashboardSP.GetAllForParent<Album>(bandId, "").Cast<Album>()));
        }

        public ActionResult AddAlbum(int bandId, string bandName)
        {
            var model = new Album
            {
                Genres = new List<Genre>(_dashboardSP.GetAll<Genre>().Cast<Genre>()),
                SelectedGenres = new List<Genre>(),
                Labels = new List<Label>(_dashboardSP.GetAll<Label>().Cast<Label>()),
                SelectedLabels = new List<Label>(),
                Musicians = new List<Musician>(_dashboardSP.GetAll<Musician>().Cast<Musician>()),
                SelectedMusicians = new List<Musician>(),
                AlbumTypes = new List<AlbumTypes>(_dashboardSP.GetAll<AlbumTypes>().Cast<AlbumTypes>()),
                BandId = bandId,
                BandName = bandName,
                Songs = new List<Song>(_dashboardSP.GetAll<Song>().Cast<Song>()),
                SelectedSongs = new List<Song>()
            };

            TempData["SelectedGenres"] = model.SelectedGenres;
            TempData["SelectedLabels"] = model.SelectedLabels;
            TempData["SelectedMusicians"] = model.SelectedMusicians;
            TempData["SelectedSongs"] = model.SelectedSongs;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            ViewBag.BandName = bandName;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddAlbum(Album album)
        {
            album.SelectedGenres = (List<Genre>)TempData["SelectedGenres"];
            album.SelectedLabels = (List<Label>)TempData["SelectedLabels"];
            album.SelectedMusicians = (List<Musician>)TempData["SelectedMusicians"];
            album.SelectedSongs = (List<Song>)TempData["SelectedSongs"];

            if (_dashboardSP.CheckDuplicate<Album>(album.AlbumName, album.Website))
            {
                TempData["CustomError"] = "Album ( " + album.AlbumName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Album ( " + album.AlbumName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Add(album);

                return RedirectToAction("Index", new { bandId = album.BandId, bandName = album.BandName });
            }

            return RedirectToAction("AddAlbum");
        }

        public ActionResult UpdateAlbum(int albumId)
        {
            var model = (Album)_dashboardSP.Find<Album>(albumId);

            TempData["SelectedGenres"] = model.SelectedGenres;
            TempData["SelectedLabels"] = model.SelectedLabels;
            TempData["SelectedMusicians"] = model.SelectedMusicians;
            TempData["SelectedSongs"] = model.SelectedSongs;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            ViewBag.BandName = model.BandName;
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateAlbum(Album album)
        {
            album.SelectedGenres = (List<Genre>)TempData["SelectedGenres"];
            album.SelectedLabels = (List<Label>)TempData["SelectedLabels"];
            album.SelectedMusicians = (List<Musician>)TempData["SelectedMusicians"];
            album.SelectedSongs = (List<Song>)TempData["SelectedSongs"];

            if (ModelState.IsValid)
            {
                _dashboardSP.Update(album);

                return RedirectToAction("Index", new { bandId = album.BandId, bandName = album.BandName});
            }

            return RedirectToAction("UpdateBand", "Band", new { albumId = album.Id });
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

        public ActionResult GetSong(int songId)
        {
            Song song = (Song)_dashboardSP.Find<Song>(songId);
            var list = (List<Song>)TempData["SelectedSongs"];
            var duplicateItem = list.SingleOrDefault(r => r.Id == songId);
            if (duplicateItem != null)
            {
                TempData["SelectedSongs"] = list;
                return null;
            }
            list.Add(song);
            TempData["SelectedSongs"] = list;

            return Json(song, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveSong(int songId)
        {
            Song song = (Song)_dashboardSP.Find<Song>(songId);
            var list = (List<Song>)TempData["SelectedSongs"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == songId);
            list.Remove(itemToRemove);
            TempData["SelectedSongs"] = list;

            return Json(song, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchSongs(string searchText)
        {
            var list = _dashboardSP.GetAllForText<Song>(searchText);
            var targetList = new List<Song>(list.Cast<Song>());

            return Json(targetList, JsonRequestBehavior.AllowGet);
        }
    }
}
