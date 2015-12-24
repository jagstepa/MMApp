using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Instrument : IModelInterface
    {
        public int Id { get; set; }

        public string InstrumentName { get; set; }

        public string Website { get; set; }
    }
}
