using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;

namespace MMApp.Web.Controllers.Books
{
    public class PublisherController : Controller
    {
        private readonly IBookRepository _dashboardSP = new BookSPRepository();

        public ActionResult Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new List<Publisher>(_dashboardSP.GetAll<Publisher>().Cast<Publisher>()));
        }

        public ActionResult AddPublisher()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new Publisher());
        }

        [HttpPost]
        public ActionResult AddPublisher(Publisher publisher)
        {
            if (_dashboardSP.CheckDuplicate<Publisher>(publisher.PublisherName))
            {
                TempData["CustomError"] = "Publisher ( " + publisher.PublisherName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Publisher ( " + publisher.PublisherName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Add(publisher);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddPublisher");
        }

        public ActionResult UpdatePublisher(int publisherId)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(_dashboardSP.Find<Publisher>(publisherId));
        }

        [HttpPost]
        public ActionResult UpdatePublisher(Publisher publisher)
        {
            var model = (Publisher)_dashboardSP.Find<Publisher>(publisher.Id);

            if (model.PublisherName == publisher.PublisherName)
            {
                TempData["CustomError"] = "Country Name didn't change!";
                ModelState.AddModelError("CustomError", "Country Name didn't change!");
            }

            if (_dashboardSP.CheckDuplicate<Publisher>(publisher.PublisherName))
            {
                TempData["CustomError"] = "Publisher ( " + publisher.PublisherName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Publisher ( " + publisher.PublisherName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Update(publisher);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdatePublisher", "Publisher", new { publisherId = publisher.Id });
        }

        public ActionResult RemovePublisher(int publisherId, string publisherName)
        {
            if (_dashboardSP.CheckDelete<Publisher>(publisherId))
            {
                TempData["CustomError"] = "Can't Delete. There are books for Publisher ( " + publisherName + " )";
                ModelState.AddModelError("CustomError", "Can't Delete. There are books for Publisher ( " + publisherName + " )");
            }
            else
            {
                _dashboardSP.Remove<Country>(publisherId);
            }

            return RedirectToAction("Index");
        }
    }
}
