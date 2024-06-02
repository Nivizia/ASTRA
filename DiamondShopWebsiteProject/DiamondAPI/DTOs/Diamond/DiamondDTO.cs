namespace DiamondAPI.DTOs.Diamond
{
    public class DiamondDTO
    {
        public string DProductId { get; set; } = null!;

        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public string? ImageUrl { get; set; }

        public string? DType { get; set; }

        public string? CaratWeight { get; set; }

        public string? Color { get; set; }

        public string? Clarity { get; set; }

        public string? Cut { get; set; }
    }
}
