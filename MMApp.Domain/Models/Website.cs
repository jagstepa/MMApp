using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Website : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [DBField]
        public string Url { get; set; }

        public int EntityId { get; set; }
    }
}
