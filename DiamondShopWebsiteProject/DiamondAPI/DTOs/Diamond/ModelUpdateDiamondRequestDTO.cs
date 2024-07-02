namespace DiamondAPI.DTOs.Diamond
{
    public class ModelUpdateDiamondRequestDTO
    {
        public string? ImageUrl { get; set; }

        public string? ShapeName { get; set; }

        public double? CaratWeight { get; set; }

        public int? Color { get; set; }

        public int? Clarity { get; set; }

        public int? Cut { get; set; }
    }
}
