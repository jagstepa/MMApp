using MMApp.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MMApp.Domain.Models
{
    public class Relationship : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [Required]
        [DBField]
        public int EntityTypeId { get; set; }

        [Required]
        [DBField]
        public int EntityRelationTypeId { get; set; }

        [Required]
        [DBField]
        public int EntityId { get; set; }

        [Required]
        [DBField]
        public int EntityRelationId { get; set; }

        public string EntityRelationValue { get; set; }
    }
}
