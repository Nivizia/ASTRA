namespace DiamondAPI.DTOs.Diamond
{
    public class CreateDiamondRequestDTO
    {
        public string? ImageUrl { get; set; }

        public string? ShapeName { get; set; }

        public double? CaratWeight { get; set; }

        public String? Color { get; set; }

        public String? Clarity { get; set; }

        public String? Cut { get; set; }
    }
}
