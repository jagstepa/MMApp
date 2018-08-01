using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Occupation : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [DBField]
        [Required]
        [Display(Name = "Occupation Name")]
        public string OccupationName { get; set; }

        [DBField]
        public string Website { get; set; }
    }
}
