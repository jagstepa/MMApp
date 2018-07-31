using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class City : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [DBField]
        [Required]
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [DBField]
        [Required]
        public int CountryId { get; set; }

        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        [DBField]
        public string Website { get; set; }

        public List<Country> Countries { get; set; }
    }
}
