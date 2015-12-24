using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Publisher : IModelInterface
    {
        public int Id { get; set; }

        public string PublisherName { get; set; }
    }
}
