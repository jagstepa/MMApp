using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Year : IModelInterface
    {
        public int YearId { get; set; }

        public int YearCode { get; set; }
    }
}
