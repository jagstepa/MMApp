using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Genre : IModelInterface
    {
        public int Id { get; set; }

        public string GenreName { get; set; }

        public string Website { get; set; }
    }
}
