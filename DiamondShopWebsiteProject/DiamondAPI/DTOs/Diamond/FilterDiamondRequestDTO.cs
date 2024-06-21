namespace DiamondAPI.DTOs.Diamond
{
    public class FilterDiamondRequestDTO
    {
        public required string DType { get; set; }
        public decimal LowerPrice { get; set; }
        public decimal UpperPrice { get; set; }
        public double LowerCaratWeight { get; set; }
        public double UpperCaratWeight { get; set; }
        public int LowerColor { get; set; }
        public int UpperColor { get; set; }
        public int LowerClarity { get; set; }
        public int UpperClarity { get; set; }
        public int LowerCut { get; set; }
        public int UpperCut { get; set; }
    }
}
