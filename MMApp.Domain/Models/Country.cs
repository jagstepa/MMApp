using System.ComponentModel.DataAnnotations;
using MMApp.Domain.Repositories;
using System.Collections.Generic;

namespace MMApp.Domain.Models
{
    public class Country : IModelInterface
    {
        [DBField]
        public int Id { get; set; }

        [DBField]
        [Required]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public List<Relationship> Websites { get; set; }

        public bool IsReadOnly { get; set; }
    }
}
