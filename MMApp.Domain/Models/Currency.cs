using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Currency : IModelInterface
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
    }
}
