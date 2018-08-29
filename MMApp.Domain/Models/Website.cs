using MMApp.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MMApp.Domain.Models
{
    public class Website : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [Required]
        [DBField]
        [Url]
        public string Url { get; set; }

        public int EntityId { get; set; }
    }
}
