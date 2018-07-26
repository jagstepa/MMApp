using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Country : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [DBField]
        [DBDuplicate]
        [Required]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        [DBField]
        public string Website { get; set; }
    }
}
