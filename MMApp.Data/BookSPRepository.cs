using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;

namespace MMApp.Data
{
    public class BookSPRepository : IBookRepository
    {
        private readonly IDbConnection _db = DBHelpers.GetDbConnection();
        List<IModelInterface> myList = new List<IModelInterface>();

        #region GetAll

        public List<IModelInterface> GetAll<T>() where T : IModelInterface
        {
            //List<IModelInterface> myList = new List<IModelInterface>();
            var type = typeof(T).Name;

            switch (type)
            {
                case "Publisher":
                    List<Publisher> publishers = _db.Query<Publisher>("sp_BookGetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(publishers);
                    break;
                case "Author":
                    List<Author> authors = _db.Query<Author>("sp_BookGetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(authors);
                    break;
                case "Book":
                    List<Book> books = _db.Query<Book>("sp_BookGetAllEntities", new { GetAllType = type }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(books);
                    break;
                case "Year":
                    List<Year> years = new List<Year>();
                    for (var i = 2009; i < 2016; i++ )
                    {
                        years.Add(new Year
                                      {
                                          YearId = i,
                                          YearCode = i
                                      });
                    }
                    myList.AddRange(years);
                    break;
            }

            return myList;
        }

        #endregion

        #region GetAllForParent

        public List<IModelInterface> GetAllForParent<T>(int id, string subType) where T : IModelInterface
        {
            //List<IModelInterface> myList = new List<IModelInterface>();
            var type = typeof(T).Name;

            switch (type)
            {
                case "Author":
                    List<Author> authors = _db.Query<Author>("sp_BookGetAllForParent", new { GetAllType = type, ParentId = id, SubType = subType }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(authors);
                    break;
            }

            return myList;
        }

        #endregion

        #region GetAllForText

        public List<IModelInterface> GetAllForText<T>(string searchText) where T : IModelInterface
        {
            //List<IModelInterface> myList = new List<IModelInterface>();
            var type = typeof(T).Name;
            if (searchText == "*") searchText = "";
            searchText = searchText + "%";

            switch (type)
            {
                case "Author":
                    List<Author> authors = _db.Query<Author>("sp_BookGetAllForText", new { GetAllType = type, SearchText = searchText }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(authors);
                    break;
                case "Book":
                    List<Book> books = _db.Query<Book>("sp_BookGetAllForText", new { GetAllType = type, SearchText = searchText }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(books);
                    break;
            }

            return myList;
        }

        #endregion

        #region Find

        public IModelInterface Find<T>(int id) where T : IModelInterface
        {
            var type = typeof(T).Name;
            IModelInterface model = null;

            switch (type)
            {
                case "Publisher":
                    model = _db.Query<Publisher>("sp_BookFindEntity", new
                    {
                        GetAllType = type,
                        TypeId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Author":
                    model = _db.Query<Author>("sp_BookFindEntity", new
                    {
                        GetAllType = type,
                        TypeId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    break;
                case "Book":
                    model = _db.Query<Book>("sp_BookFindEntity", new
                    {
                        GetAllType = type,
                        TypeId = id
                    }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    object objBook = model;
                    Book book = (Book)objBook;
                    book.Publishers = new List<Publisher>(GetAll<Publisher>().Cast<Publisher>());
                    book.Authors = new List<Author>();
                    book.SelectedAuthors = new List<Author>(GetAllForParent<Author>(id, type).Cast<Author>());
                    book.YearList = new List<Year>(GetAll<Year>().Cast<Year>());
                    break;
                
            }

            return model;
        }

        #endregion

        #region Add

        public void Add<T>(T value) where T : IModelInterface
        {
            var type = typeof(T).Name;
            object obj;
            string values = "";
            bool genericSave = true;

            switch (type)
            {
                case "Publisher":
                    obj = value;
                    Publisher publisher = (Publisher)obj;
                    values = "PublisherName#" + publisher.PublisherName;
                    break;
                case "Author":
                    obj = value;
                    Author author = (Author)obj;
                    values = "AuthorName#" + author.AuthorName;
                    break;
                case "Book":
                    genericSave = false;
                    obj = value;
                    Book book = (Book)obj;
                    values = "BookName#" + book.BookName + "@ShortDescription#" + book.ShortDescription + "@BookDescription#" + book.BookDescription + 
                        "@ISBN#" + book.ISBN10 + "@Year#" + book.Year + "@Pages#" + book.Pages + "@FileSize#" + book.FileSize +
                        "@FileFormat#" + book.FileFormat + "@BookPicture#" + book.BookPicture +
                        "@PublisherId#" + book.PublisherId;
                    var bookId = _db.Query<int>("sp_BookAddEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).Single();

                    foreach (var selectedAuthor in book.SelectedAuthors)
                    {
                        const string bookType = "SelectedAuthors";
                        values = "BookId#" + bookId + "@AuthorId#" + selectedAuthor.Id;
                        _db.Execute("sp_BookAddEntity", new { GetAllType = bookType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    break;
                case "Category":
                    obj = value;
                    Category category = (Category)obj;
                    values = "CategoryName#" + category.CategoryName + "@Website#" + category.Website;
                    break;
                case "Currency":
                    obj = value;
                    Currency currency = (Currency)obj;
                    values = "CurrencyName#" + currency.CurrencyName + "@CurrencySymbol#" + currency.CurrencySymbol;
                    break;
                case "Format":
                    obj = value;
                    BookFormat bookFormat = (BookFormat)obj;
                    values = "FormatName#" + bookFormat.FormatName;
                    break;
            }

            if (genericSave)
            {
                _db.Execute("sp_BookAddEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region Update

        public void Update<T>(T value) where T : IModelInterface
        {
            var type = typeof(T).Name;
            object obj;
            string values = "";
            bool genericUpdate = true;

            switch (type)
            {
                case "Publisher":
                    obj = value;
                    Publisher publisher = (Publisher)obj;
                    values = "PublisherName#" + publisher.PublisherName + "@Id#" + publisher.Id;
                    break;
                case "Author":
                    obj = value;
                    Author author = (Author)obj;
                    values = "AuthorName#" + author.AuthorName + "@Id#" + author.Id;
                    break;
                case "Book":
                    genericUpdate = false;
                    obj = value;
                    Book book = (Book)obj;
                    values = "BookName#" + book.BookName + "@BookDescription#" + book.BookDescription +
                        "@ISBN#" + book.ISBN10 + "@Year#" + book.Year + "@Pages#" + book.Pages + "@FileSize#" + book.FileSize +
                        "@ShortDescription#" + book.ShortDescription + "@BookPicture#" + book.BookPicture +
                        "@FileFormat#" + book.FileFormat + "@PublisherId#" + book.PublisherId + "@Id#" + +book.Id;
                    _db.Execute("sp_BookUpdateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);

                    const string subTypeBook = "SelectedAuthors";
                    _db.Execute("sp_BookDeleteEntity", new { GetAllType = subTypeBook, TypeId = book.Id }, commandType: CommandType.StoredProcedure);
                    foreach (var selectedAuthor in book.SelectedAuthors)
                    {
                        const string bookType = "SelectedAuthors";
                        values = "BookId#" + book.Id + "@AuthorId#" + selectedAuthor.Id;
                        _db.Execute("sp_BookAddEntity", new { GetAllType = bookType, Parameters = values }, commandType: CommandType.StoredProcedure);
                    }
                    break;
            }

            if (genericUpdate)
            {
                _db.Execute("sp_BookUpdateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region Remove

        public void Remove<T>(int id) where T : IModelInterface
        {
            var type = typeof(T).Name;

            _db.Execute("sp_BookDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure);
        }

        #endregion

        #region Check Delete

        public bool CheckDelete<T>(int id) where T : IModelInterface
        {
            var type = typeof(T).Name;
            bool result = false;

            switch (type)
            {
                case "Publisher":
                    var publisherId = _db.Query<int>("sp_BookCheckDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (publisherId > 0) result = true;
                    break;
                case "Author":
                    var authorId = _db.Query<int>("sp_BookCheckDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (authorId > 0) result = true;
                    break;
                case "Book":
                    var bookId = _db.Query<int>("sp_BookCheckDeleteEntity", new { GetAllType = type, TypeId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (bookId > 0) result = true;
                    break;
            }

            return result;
        }

        #endregion

        #region Check Duplicate

        public bool CheckDuplicate<T>(string name) where T : IModelInterface
        {
            var type = typeof(T).Name;
            string values;
            bool result = false;

            switch (type)
            {
                case "Publisher":
                    values = "PublisherName#" + name;
                    var publisherId = _db.Query<int>("sp_BookCheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (publisherId > 0) result = true;
                    break;
                case "Author":
                    values = "AuthorName#" + name;
                    var authorId = _db.Query<int>("sp_BookCheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (authorId > 0) result = true;
                    break;
                case "Book":
                    values = "BookName#" + name;
                    var bookId = _db.Query<int>("sp_BookCheckDuplicateEntity", new { GetAllType = type, Parameters = values }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    if (bookId > 0) result = true;
                    break;
            }

            return result;
        }

        #endregion

        #region Filters

        public List<IModelInterface> GetFilterItems<T>(string filterType, string filterItem) where T : IModelInterface
        {
            //List<IModelInterface> myList = new List<IModelInterface>();
            var type = typeof(T).Name;

            switch (type)
            {
                case "Publisher":
                    List<FilterItem> filterItems = _db.Query<FilterItem>("sp_BookFilters", new { GetAllType = filterType, @FilterItem = filterItem }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(filterItems);
                    break;
                case "Book":
                    List<Book> books = _db.Query<Book>("sp_BookFilters", new { GetAllType = "Book", @FilterItem = filterItem }, commandType: CommandType.StoredProcedure).ToList();
                    myList.AddRange(books);
                    break;
            }

            return myList;
        }

        #endregion
    }
}
