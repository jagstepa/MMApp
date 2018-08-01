using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class GenreController : Controller
    {
        IMusicRepository _db;
        private string errorMessage;

        public GenreController(IMusicRepository db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<Genre>(_db.GetAll<Genre>().Cast<Genre>()));
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
            if (_db.CheckDuplicate<Genre>(genre))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(genre.GenreName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Add<Genre>(genre);

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

            return View(_db.Find<Genre>(genreId));
        }

        [HttpPost]
        public ActionResult UpdateGenre(Genre genre)
        {
            var model = (Genre)_db.Find<Genre>(genre.Id);

            if (_db.CheckDuplicate<Genre>(genre))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Genre>(genre.GenreName, ErrorMessageType.Changes);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Update<Genre>(genre);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateGenre", "Genre", new { genreId = genre.Id });
        }

        public ActionResult RemoveGenre(int genreId, string genreName)
        {
            var model = (Genre)_db.Find<Genre>(genreId);

            if (_db.CheckDelete<Genre>(model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Genre>(genreName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _db.Remove<Genre>(model);
            }

            return RedirectToAction("Index");
        }
    }
}
