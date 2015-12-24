using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Band : IModelInterface
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Band Name")]
        public string BandName { get; set; }

        [Display(Name = "Also Known As")]
        public string AlsoKnownAs { get; set; }

        public string Website { get; set; }

        [Required]
        public int CityId { get; set; }

        public string CityName { get; set; }

        [Required]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public List<Country> Countries { get; set; }

        public List<City> Cities { get; set; }

        public int? GenreId { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Genre> SelectedGenres { get; set; }

        public int? LabelId { get; set; }

        public List<Label> Labels { get; set; }

        public List<Label> SelectedLabels { get; set; }

        public int? MusicianId { get; set; }

        public List<Musician> Musicians { get; set; }

        public List<Musician> SelectedMusicians { get; set; }

        [Display(Name = "From Year")]
        public string YearFrom { get; set; }

        [Display(Name = "To Year")]
        public string YearTo { get; set; }

        public List<MusicianActivity> MusicianActivity { get; set; }
    }
}
