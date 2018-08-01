using MMApp.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MMApp.Domain.Models
{
    public class Instrument : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [DBField]
        [Required]
        [Display(Name = "Instrument Name")]
        public string InstrumentName { get; set; }

        [DBField]
        public string Website { get; set; }
    }
}
