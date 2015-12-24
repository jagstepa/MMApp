using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Album : IModelInterface
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Album Name")]
        public string AlbumName { get; set; }

        [Required]
        public int TypeId { get; set; }

        public List<AlbumTypes> AlbumTypes { get; set; }

        public string TypeName { get; set; }

        public string Year { get; set; }

        public string Website { get; set; }

        public string Released { get; set; }
        public string Recorded { get; set; }
        public string Length { get; set; }

        public int? GenreId { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Genre> SelectedGenres { get; set; }

        public int? LabelId { get; set; }

        public List<Label> Labels { get; set; }

        public List<Label> SelectedLabels { get; set; }

        public int? MusicianId { get; set; }

        public List<Musician> Musicians { get; set; }

        public List<Musician> SelectedMusicians { get; set; }

        public string BandName { get; set; }

        public int BandId { get; set; }

        public int? SongId { get; set; }

        public List<Song> Songs { get; set; }

        public List<Song> SelectedSongs { get; set; }

    }
}
