using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class AlbumTypes : IModelInterface
    {
        public int Id { get; set; }

        [Display(Name = "Type Name")]
        public string TypeName { get; set; }
    }
}
