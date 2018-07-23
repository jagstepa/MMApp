using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Website : IModelInterface
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Url { get; set; }
    }
}
