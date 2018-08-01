using MMApp.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MMApp.Domain.Models
{
    public class Genre : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [DBField]
        [Required]
        [Display(Name = "Genre Name")]
        public string GenreName { get; set; }

        [DBField]
        public string Website { get; set; }
    }
}
