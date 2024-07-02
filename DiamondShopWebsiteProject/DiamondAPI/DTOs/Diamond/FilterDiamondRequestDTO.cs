namespace DiamondAPI.DTOs.Diamond
{
    public class FilterDiamondRequestDTO
    {
        public required string ShapeName { get; set; }
        public decimal? LowerPrice { get; set; }
        public decimal? UpperPrice { get; set; }
        public double? LowerCaratWeight { get; set; }
        public double? UpperCaratWeight { get; set; }
        public String? LowerColor { get; set; }
        public String? UpperColor { get; set; }
        public String? LowerClarity { get; set; }
        public String? UpperClarity { get; set; }
        public String? LowerCut { get; set; }
        public String? UpperCut { get; set; }
    }
}
