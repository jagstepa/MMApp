using System.Linq;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Domain.ViewModel;
using MMApp.Web.Helpers;
using PagedList;

namespace MMApp.Web.Controllers.Books
{
    public class AuthorController : Controller
    {
        private readonly IBookRepository _dashboardSP = new BookSPRepository();
        private readonly InMemoryCache _cache = new InMemoryCache();

        public ActionResult Index(string searchText = "", int page = 1)
        {
            var list = new AuthorViewModelList();
            var cachedList = _cache.GetOrSet("AuthorList", () => _dashboardSP.GetAll<Author>().Cast<Author>());
            var cachedSearch = _cache.Get("AuthorSearch");
            string outSearchText;
            bool outRefreshAuthorList;

            Helpers.Helpers.CheckCashedSearchText(searchText, cachedSearch, "AuthorSearch", out outSearchText, out outRefreshAuthorList);

            if (outRefreshAuthorList)
            {
                _cache.RemoveItem("AuthorList");
                cachedList = _cache.GetOrSet("AuthorList", () => _dashboardSP.GetAllForText<Author>(searchText).Cast<Author>());
            }

            list.AuthorPaginated = cachedList.ToPagedList(page, 20);
            list.SearchText = outSearchText;
            return View(list);
        }

        public ActionResult AddAuthor()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(new Author());
        }

        [HttpPost]
        public ActionResult AddAuthor(Author author)
        {
            if (_dashboardSP.CheckDuplicate<Author>(author.AuthorName))
            {
                TempData["CustomError"] = "Author ( " + author.AuthorName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Author ( " + author.AuthorName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Add(author);
                _cache.RemoveItem("AuthorList");

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddAuthor");
        }

        public ActionResult UpdateAuthor(int authorId)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(_dashboardSP.Find<Author>(authorId));
        }

        [HttpPost]
        public ActionResult UpdateAuthor(Author author)
        {
            var model = (Author)_dashboardSP.Find<Author>(author.Id);

            if (model.AuthorName == author.AuthorName)
            {
                TempData["CustomError"] = "Author Name didn't change!";
                ModelState.AddModelError("CustomError", "Author Name didn't change!");
            }

            if (_dashboardSP.CheckDuplicate<Author>(author.AuthorName))
            {
                TempData["CustomError"] = "Author ( " + author.AuthorName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Author ( " + author.AuthorName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Update(author);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateAuthor", "Author", new { authorId = author.Id });
        }

        public ActionResult RemoveAuthor(int authorId, string authorName)
        {
            if (_dashboardSP.CheckDelete<Author>(authorId))
            {
                TempData["CustomError"] = "Can't Delete. There are books for Author ( " + authorName + " )";
                ModelState.AddModelError("CustomError", "Can't Delete. There are books for Author ( " + authorName + " )");
            }
            else
            {
                _dashboardSP.Remove<Country>(authorId);
                _cache.RemoveItem("AuthorList");
            }

            return RedirectToAction("Index");
        }
    }
}
