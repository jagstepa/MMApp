using MMApp.Domain.Models;
using PagedList;

namespace MMApp.Domain.ViewModel
{
    public class AuthorViewModel
    {
        public Author Author { get; set; }

        public AuthorViewModel (Author author)
        {
            Author = author;
        }
    }

    public class AuthorViewModelList
    {
        public IPagedList<Author> AuthorPaginated { get; set; }

        public string SearchText { get; set; }
    }
}
