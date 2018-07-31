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
    public class GenreController : Controller
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
            if (_dashboardSP.CheckDuplicate<Genre>(genre))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(genre.GenreName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Add<Genre>(genre);

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

            if (_dashboardSP.CheckDuplicate<Genre>(genre))
            {
                TempData["CustomError"] = "Genre ( " + genre.GenreName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Genre ( " + genre.GenreName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Update<Genre>(genre);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateGenre", "Genre", new { genreId = genre.Id });
        }

        public ActionResult RemoveGenre(int genreId, string genreName)
        {
            var model = (Genre)_dashboardSP.Find<Genre>(genreId);

            if (_dashboardSP.CheckDelete<Genre>(model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Genre>(genreName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _dashboardSP.Remove<Genre>(model);
            }

            return RedirectToAction("Index");
        }
    }
}
