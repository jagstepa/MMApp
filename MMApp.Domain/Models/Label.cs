using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Label : IModelInterface
    {
        public int Id { get; set; }

        public string LabelName { get; set; }

        public string Website { get; set; }
    }
}
