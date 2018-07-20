using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Category : IModelInterface
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Website { get; set; }
    }
}
