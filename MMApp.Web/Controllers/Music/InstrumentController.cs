using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Controllers.Music
{
    public class InstrumentController : Controller
    {
        IMusicRepository _db;
        private string errorMessage;

        public InstrumentController(IMusicRepository db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<Instrument>(_db.GetAll<Instrument>().Cast<Instrument>()));
        }

        public ActionResult AddInstrument()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new Instrument());
        }

        [HttpPost]
        public ActionResult AddInstrument(Instrument instrument)
        {
            if (_db.CheckDuplicate<Instrument>(instrument))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(instrument.InstrumentName, ErrorMessageType.Duplicate);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Add<Instrument>(instrument);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddInstrument");
        }

        public ActionResult UpdateInstrument(int instrumentId)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(_db.Find<Instrument>(instrumentId));
        }

        [HttpPost]
        public ActionResult UpdateInstrument(Instrument instrument)
        {
            var model = (Instrument)_db.Find<Instrument>(instrument.Id);

            if (Helper.CheckForChanges<Country>(instrument, model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(instrument.InstrumentName, ErrorMessageType.Changes);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _db.Update<Instrument>(instrument);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateInstrument", "Instrument", new { instrumentId = instrument.Id });
        }

        public ActionResult RemoveInstrument(int instrumentId, string instrumentName)
        {
            var model = (Instrument)_db.Find<Instrument>(instrumentId);

            if (_db.CheckDelete<Instrument>(model))
            {
                errorMessage = ErrorMessages.GetErrorMessage<Country>(instrumentName, ErrorMessageType.Delete);
                TempData["CustomError"] = errorMessage;
                ModelState.AddModelError("CustomError", errorMessage);
            }
            else
            {
                _db.Remove<Instrument>(model);
            }

            return RedirectToAction("Index");
        }
    }
}
