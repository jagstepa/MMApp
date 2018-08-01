using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Album : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [DBField]
        [Required]
        [Display(Name = "Album Name")]
        public string AlbumName { get; set; }

        [DBField]
        [Required]
        public int TypeId { get; set; }

        public List<AlbumType> AlbumTypes { get; set; }

        public string TypeName { get; set; }

        [DBField]
        public string Year { get; set; }

        [DBField]
        public string Website { get; set; }

        [DBField]
        public string Released { get; set; }

        [DBField]
        public string Recorded { get; set; }

        [DBField]
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
