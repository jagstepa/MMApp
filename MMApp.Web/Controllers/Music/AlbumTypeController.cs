using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class AlbumTypeController : Controller
    {
        IMusicRepository _db;
        private string errorMessage;

        public AlbumTypeController(IMusicRepository db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<AlbumType>(_db.GetAll<AlbumType>().Cast<AlbumType>()));
        }

        public ActionResult AddAlbumType()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new AlbumType());
        }

        [HttpPost]
        public ActionResult AddAlbumType(AlbumType albumType)
        {
            if (_db.CheckDuplicate<AlbumType>(albumType))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(albumType.TypeName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Add<AlbumType>(albumType);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddAlbumType");
        }

        public ActionResult UpdateAlbumType(int albumTypeId)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(_db.Find<AlbumType>(albumTypeId));
        }

        [HttpPost]
        public ActionResult UpdateAlbumType(AlbumType albumType)
        {
            var model = (AlbumType)_db.Find<AlbumType>(albumType.Id);

            if (Helper.CheckForChanges<Country>(albumType, model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<AlbumType>(albumType.TypeName, ErrorMessageType.Changes);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Update<AlbumType>(albumType);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateAlbumType", "AlbumType", new { albumTypeId = albumType.Id });
        }

        public ActionResult RemoveAlbumType(int albumTypeId, string TypeName)
        {
            var model = (AlbumType)_db.Find<AlbumType>(albumTypeId);

            if (_db.CheckDelete<AlbumType>(model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<AlbumType>(TypeName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _db.Remove<Country>(model);
            }

            return RedirectToAction("Index");
        }
    }
}