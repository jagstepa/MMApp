using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class MusicianActivity : IModelInterface
    {
        public int Id { get; set; }

        public int BandId { get; set; }

        public int MusicianId { get; set; }

        public string MusicianStageName { get; set; }

        public string YearFrom { get; set; }

        public string YearTo { get; set; }

        public string Activity { get; set; }
    }
}
