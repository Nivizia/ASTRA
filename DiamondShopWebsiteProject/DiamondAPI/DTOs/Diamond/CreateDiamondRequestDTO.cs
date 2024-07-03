namespace DiamondAPI.DTOs.Diamond
{
    public class CreateDiamondRequestDTO
    {
        public string? ImageUrl { get; set; }

        public string? Shape { get; set; }

        public double? CaratWeight { get; set; }

        public String? Color { get; set; }

        public String? Clarity { get; set; }

        public String? Cut { get; set; }

        public bool? Available { get; set; }
    }
}
