using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;
using PagedList;

namespace MMApp.Domain.Models
{
    public class Book : IModelInterface
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Book Description")]
        public string BookDescription { get; set; }

        public string ISBN { get; set; }

        public int Year { get; set; }

        public List<Year> YearList { get; set; }

        public int Pages { get; set; }

        [Display(Name = "File Size (MB)")]
        public double FileSize { get; set; }

        [Display(Name = "File Format")]
        public string FileFormat { get; set; }

        public string Website { get; set; }

        public int PublisherId { get; set; }

        [Display(Name = "Publisher Name")]
        public string PublisherName { get; set; }

        public int? AuthorId { get; set; }

        public List<Author> Authors { get; set; }

        [Display(Name = "Selected Authors")]
        public List<Author> SelectedAuthors { get; set; }

        public List<Publisher> Publishers { get; set; }

        [Display(Name = "Book Picture")]
        public string BookPicture { get; set; }

        public int? Page { get; set; }

        public IPagedList SearchResults { get; set; }
    }
}
