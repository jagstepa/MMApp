using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using MMApp.Web.Helpers;
using PagedList;
using MMApp.Domain.ViewModel;

namespace MMApp.Web.Controllers.Books
{
    public class BookController : Controller
    {
        private readonly IBookRepository _dashboardSP = new BookSPRepository();
        private readonly InMemoryCache _cache = new InMemoryCache();

        public ActionResult Index(string searchText = "", int page = 1, string filterType = "", string filterItem = "")
        {
            var list = new BookViewModelList();
            var cachedList = _cache.GetOrSet("BookList", () => _dashboardSP.GetAll<Book>().Cast<Book>());
            var cachedSearch = _cache.Get("BookSearch");
            string outSearchText;
            bool outRefreshAuthorList;
            Helper.CheckCashedSearchText(searchText, cachedSearch, "BookSearch", out outSearchText, out outRefreshAuthorList);

            if (outRefreshAuthorList)
            {
                _cache.RemoveItem("BookList");
                cachedList = _cache.GetOrSet("BookList", () => _dashboardSP.GetAllForText<Book>(searchText).Cast<Book>());
            }

            var cahedFilterType = _cache.Get("FilterType");
            var cahedFilterItem = _cache.Get("FilterItem");
            string outFilterType;
            string outFilterItem;
            bool outRefreshModelList;
            Helper.CheckCashedSearchByType(filterType, filterItem, cahedFilterType, cahedFilterItem, "FilterType", "FilterItem", 
                out outFilterType, out outFilterItem, out outRefreshModelList);

            if (outRefreshModelList)
            {
                _cache.RemoveItem("BookList");
                cachedList = _cache.GetOrSet("BookList", () => _dashboardSP.GetFilterItems<Book>(filterType, filterItem).Cast<Book>());
            }

            list.BookPaginated = cachedList.ToPagedList(page, 20);
            list.SearchText = outSearchText;
            list.FilterType = outFilterType;
            list.FilterItem = outFilterItem;
            return View(list);
        }

        public ActionResult AddBook()
        {
            var book = new Book
                           {
                               Publishers = new List<Publisher>(_dashboardSP.GetAll<Publisher>().Cast<Publisher>()),
                               Authors = new List<Author>(),
                               SelectedAuthors = new List<Author>(),
                               YearList = new List<Year>(_dashboardSP.GetAll<Year>().Cast<Year>()),
                               Year = 2015
                           };

            TempData["SelectedAuthors"] = book.SelectedAuthors;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(book);
        }

        [HttpPost]
        public ActionResult AddBook(Book book, HttpPostedFileBase file)
        {
            book.SelectedAuthors = (List<Author>)TempData["SelectedAuthors"];

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                if (fileName != null)
                {
                    var path = Path.Combine(Server.MapPath("~/Uploads/Books"), fileName);
                    file.SaveAs(path);
                    path = Url.Content(Path.Combine("~/Uploads/Books", fileName));
                    book.BookPicture = path;
                }
            }

            if (_dashboardSP.CheckDuplicate<Book>(book.BookName))
            {
                TempData["CustomError"] = "Book ( " + book.BookName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Book ( " + book.BookName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Add(book);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddBook");
        }

        public ActionResult UpdateBook(int bookId)
        {
            var book = (Book)_dashboardSP.Find<Book>(bookId);
            TempData["SelectedAuthors"] = book.SelectedAuthors;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            ViewBag.BookName = book.BookName;
            return View(book);
        }

        [HttpPost]
        public ActionResult UpdateBook(Book book)
        {
            book.SelectedAuthors = (List<Author>)TempData["SelectedAuthors"];

            if (ModelState.IsValid)
            {
                _dashboardSP.Update(book);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateBook", "Book", new { bookId = book.Id });
        }

        public ActionResult RemoveBook(int bookId, string bookName)
        {
            if (_dashboardSP.CheckDelete<Book>(bookId))
            {
                TempData["CustomError"] = "Can't Delete. There are books for Author ( " + bookName + " )";
                ModelState.AddModelError("CustomError", "Can't Delete. There are books for Author ( " + bookName + " )");
            }
            else
            {
                _dashboardSP.Remove<Book>(bookId);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ShowBook(int bookId)
        {
            var book = (Book)_dashboardSP.Find<Book>(bookId);

            return View(book);
        }

        public ActionResult GetAuthor(int authorId)
        {
            Author author = (Author)_dashboardSP.Find<Author>(authorId);
            var list = (List<Author>)TempData["SelectedAuthors"];
            var duplicateItem = list.SingleOrDefault(r => r.Id == authorId);
            if (duplicateItem != null)
            {
                TempData["SelectedAuthors"] = list;
                return null;
            }
            list.Add(author);
            TempData["SelectedAuthors"] = list;

            return Json(author, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveAuthor(int authorId)
        {
            Author author = (Author)_dashboardSP.Find<Author>(authorId);
            var list = (List<Author>)TempData["SelectedAuthors"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == authorId);
            list.Remove(itemToRemove);
            TempData["SelectedAuthors"] = list;

            return Json(author, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchAuthor(string searchText)
        {
            var list = _dashboardSP.GetAllForText<Author>(searchText);
            var targetList = new List<Author>(list.Cast<Author>());

            return Json(targetList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFilter(string filterType)
        {
            var filterItems = new List<FilterItem>();
            if (filterType == "Publisher")
            {
                filterItems = new List<FilterItem>(_dashboardSP.GetFilterItems<Publisher>(filterType, "").Cast<FilterItem>());
            }
            TempData["FilterItems"] = filterItems;

            return Json(filterItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Reset()
        {
            _cache.RemoveItem("BookList");
            _cache.RemoveItem("BookSearch");
            _cache.RemoveItem("FilterType");
            _cache.RemoveItem("FilterItem");

            return Json("Cache removed", JsonRequestBehavior.AllowGet);
        }
    }
}
