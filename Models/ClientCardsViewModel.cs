namespace Supermarket.Models
{
    public class ClientCardsViewModel
    {
        public required List<ClientCard> ClientCards { get; set; }
        public required PagingInfo PagingInfo { get; set; }

        public string SearchName { get; set; } = string.Empty;
        public string SearchEstado { get; set; } = string.Empty;
    }
}
