using System.Collections.Generic;
using MMApp.Domain.Models;
using PagedList;

namespace MMApp.Domain.ViewModel
{
    public class BookViewModel
    {
        public Book Book { get; set; }

        public BookViewModel(Book book)
        {
            Book = book;
        }
    }

    public class BookViewModelList
    {
        public IPagedList<Book> BookPaginated { get; set; }

        public string SearchText { get; set; }

        public string FilterType { get; set; }

        public List<FilterType> FilterTypes { get; set; }

        public string FilterItem { get; set; }

        public List<FilterItem> FilterItems { get; set; }
    }
}
