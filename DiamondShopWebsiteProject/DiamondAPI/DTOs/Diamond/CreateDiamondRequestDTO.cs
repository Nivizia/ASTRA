namespace DiamondAPI.DTOs.Diamond
{
    public class CreateDiamondRequestDTO
    {
        public decimal? Price { get; set; }

        public string? ImageUrl { get; set; }

        public string? DType { get; set; }

        public double? CaratWeight { get; set; }

        public int? Color { get; set; }

        public int? Clarity { get; set; }

        public int? Cut { get; set; }
    }
}
