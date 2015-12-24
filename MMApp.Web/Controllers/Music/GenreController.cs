using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;

namespace MMApp.Web.Controllers.Music
{
    public class GenreController : Controller
    {
        //private readonly IMusicRepository _dashboard = new MusicRepository();
        private readonly IMusicRepository _dashboardSP = new MusicSPRepository();

        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<Genre>(_dashboardSP.GetAll<Genre>().Cast<Genre>()));
        }

        public ActionResult AddGenre()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new Genre());
        }

        [HttpPost]
        public ActionResult AddGenre(Genre genre)
        {
            if (_dashboardSP.CheckDuplicate<Genre>(genre.GenreName, genre.Website))
            {
                TempData["CustomError"] = "Genre ( " + genre.GenreName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Genre ( " + genre.GenreName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Add(genre);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddGenre");
        }

        public ActionResult UpdateGenre(int genreId)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(_dashboardSP.Find<Genre>(genreId));
        }

        [HttpPost]
        public ActionResult UpdateGenre(Genre genre)
        {
            var model = (Genre)_dashboardSP.Find<Genre>(genre.Id);

            if (model.GenreName == genre.GenreName && model.Website == genre.Website)
            {
                TempData["CustomError"] = "Genre Name didn't change!";
                ModelState.AddModelError("CustomError", "Genre Name didn't change!");
            }

            if (_dashboardSP.CheckDuplicate<Genre>(genre.GenreName, genre.Website))
            {
                TempData["CustomError"] = "Genre ( " + genre.GenreName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Genre ( " + genre.GenreName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Update(genre);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateGenre", "Genre", new { genreId = genre.Id });
        }

        public ActionResult RemoveGenre(int genreId, string genreName)
        {
            var result = _dashboardSP.CheckDelete<Genre>(genreId);

            if (result)
            {
                TempData["CustomError"] = "Can't Delete. There are genres for Musician ( " + genreName + " )";
                ModelState.AddModelError("CustomError", "Can't Delete. There are genres for Musician ( " + genreName + " )");
            }
            else
            {
                _dashboardSP.Remove<Genre>(genreId);
            }

            return RedirectToAction("Index");
        }
    }
}
