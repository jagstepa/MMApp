using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class FilterItem : IModelInterface
    {
        public string FilterId { get; set; }

        public string FilterText { get; set; }
    }
}
