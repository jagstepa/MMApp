using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class AlbumType : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [DBField]
        [Required]
        [Display(Name = "Type Name")]
        public string TypeName { get; set; }
    }
}
