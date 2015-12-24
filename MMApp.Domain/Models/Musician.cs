using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Musician : IModelInterface
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Stage Name")]
        public string StageName { get; set; }

        [Display(Name = "Birth Name")]
        public string BirthName { get; set; }

        public string Website { get; set; }

        [Display(Name = "Active From")]
        public string YearsActiveFrom { get; set; }

        [Display(Name = "Active To")]
        public string YearsActiveTo { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }

        [Display(Name = "Date of death")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOD { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int CountryId { get; set; }

        public int? GenreId { get; set; }

        public int? OccupationId { get; set; }

        public int? InstrumentId { get; set; }

        public int? LabelId { get; set; }

        public List<Country> Countries { get; set; }

        public List<City> Cities { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Genre> SelectedGenres { get; set; }

        public List<Occupation> Occupations { get; set; }

        public List<Occupation> SelectedOccupations { get; set; }

        public List<Instrument> Instruments { get; set; }

        public List<Instrument> SelectedInstruments { get; set; }

        public List<Label> Labels { get; set; }

        public List<Label> SelectedLabels { get; set; }

        public string MusicianActivity { get; set; }
    }
}
