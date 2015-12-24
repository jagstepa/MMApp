using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Occupation : IModelInterface
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Occupation Name")]
        public string OccupationName { get; set; }

        public string Website { get; set; }
    }
}
