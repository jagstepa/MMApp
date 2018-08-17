using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Relationship : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [DBField]
        public int EntityTypeId { get; set; }

        [DBField]
        public int EntityRelationTypeId { get; set; }

        [DBField]
        public int EntityId { get; set; }

        [DBField]
        public int EntityRelationId { get; set; }

        public string EntityRelationValue { get; set; }
    }
}
