using MMApp.Domain.Repositories;

namespace MMApp.Domain.Models
{
    public class Seller : IModelInterface
    {
        public int Id { get; set; }
        public string SellerName { get; set; }
        public string Website { get; set; }
    }
}
