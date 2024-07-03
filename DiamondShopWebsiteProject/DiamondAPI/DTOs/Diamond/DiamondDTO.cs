namespace DiamondAPI.DTOs.Diamond
{
    public class DiamondDTO
    {
        public Guid DProductId { get; set; }

        public decimal? Price { get; set; }

        public string? ImageUrl { get; set; }

        public String? Shape { get; set; }

        public double? CaratWeight { get; set; }

        public String? Color { get; set; }

        public String? Clarity { get; set; }

        public String? Cut { get; set; }

        public bool? Available { get; set; }
    }
}
