using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Song : IModelInterface
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Song Name")]
        public string SongName { get; set; }

        public string Length { get; set; }

        public int? MusicianId { get; set; }

        [Display(Name = "Written By")]
        public List<Musician> Musicians { get; set; }

        public List<Musician> SelectedMusicians { get; set; }
    }
}
