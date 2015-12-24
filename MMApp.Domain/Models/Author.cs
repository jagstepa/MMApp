using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Author : IModelInterface
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

    }
}
