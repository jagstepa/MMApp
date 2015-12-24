using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Country : IModelInterface
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public string Website { get; set; }
    }
}
