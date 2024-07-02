namespace DiamondAPI.DTOs.Ring
{
    public class RingDTO
    {
        public Guid RingId { get; set; }

        public string? RingType { get; set; }

        public string? RingSubtype { get; set; }

        public string? FrameType { get; set; }

        public string? MetalType { get; set; }

        public string? RingSize { get; set; }

        public string? RingName { get; set; }

        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }

        public string? ImageUrl { get; set; }
    }
}