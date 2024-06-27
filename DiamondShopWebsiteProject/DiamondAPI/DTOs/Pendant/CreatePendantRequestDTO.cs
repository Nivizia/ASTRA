namespace DiamondAPI.DTOs.Pendant
{
    public class CreatePendantRequestDTO
    {
        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }

        public string? ImageUrl { get; set; }

        public string? ChainType { get; set; }

        public string? ChainLength { get; set; }

        public string? ClaspType { get; set; }
    }
}
