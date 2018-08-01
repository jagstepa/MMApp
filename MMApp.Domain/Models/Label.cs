using MMApp.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MMApp.Domain.Models
{
    public class Label : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [DBField]
        [Required]
        [Display(Name = "Label Name")]
        public string LabelName { get; set; }

        [DBField]
        public string Website { get; set; }
    }
}
