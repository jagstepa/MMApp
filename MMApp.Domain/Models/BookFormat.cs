using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class BookFormat : IModelInterface
    {
        public int Id { get; set; }
        public string FormatName { get; set; }
    }
}
