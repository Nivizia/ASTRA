namespace DiamondAPI.Services
{
    public class DiamondCalculatorService
    {
        public decimal CalculateDiamondPrice(double? carat, string? color, string? clarity, string? cut)
        {
            if (!carat.HasValue)
            {
                throw new ArgumentNullException(nameof(carat), "Carat cannot be null.");
            }

            decimal caratDecimal = (decimal)carat;
            color ??= "I";
            clarity ??= "SI1";
            cut ??= "fair";

            return CalculatePrice(caratDecimal, color, clarity, cut);
        }

        internal static decimal? CalculateDiamondPriceToDiamondDTO(double? caratWeight, int? color, int? clarity, int? cut)
        {
            if (!caratWeight.HasValue || !color.HasValue || !clarity.HasValue || !cut.HasValue)
            {
                throw new ArgumentNullException("One or more required parameters are null.");
            }

            // Convert int values to corresponding string values
            string colorStr = color.Value switch
            {
                1 => "K",
                2 => "J",
                3 => "I",
                4 => "H",
                5 => "G",
                6 => "F",
                7 => "E",
                8 => "D",
                _ => throw new ArgumentOutOfRangeException(nameof(color), "Invalid color value.")
            };

            string clarityStr = clarity.Value switch
            {
                1 => "SI2",
                2 => "SI1",
                3 => "VS2",
                4 => "VS1",
                5 => "VVS2",
                6 => "VVS1",
                7 => "IF",
                8 => "FL",
                _ => throw new ArgumentOutOfRangeException(nameof(clarity), "Invalid clarity value.")
            };

            string cutStr = cut.Value switch
            {
                1 => "Fair",
                2 => "Good",
                3 => "Very Good",
                4 => "Excellent",
                _ => throw new ArgumentOutOfRangeException(nameof(cut), "Invalid cut value.")
            };

            return CalculatePrice((decimal)caratWeight, colorStr, clarityStr, cutStr);
        }

        private static decimal CalculatePrice(decimal carat, string color, string clarity, string cut)
        {
            // Base price per carat
            decimal basePrice = 1000m;
            decimal caratPriceAdjustment = carat * basePrice;

            // Price adjustments based on characteristics
            decimal colorPriceAdjustment = color.ToUpper() switch
            {
                "D" => 3.3m,
                "E" => 2.6m,
                "F" => 2.3m,
                "G" => 2m,
                "H" => 1.7m,
                "I" => 1.5m,
                "J" => 1.2m,
                "K" => 1.0m,
                _ => 1.0m
            };

            decimal clarityPriceAdjustment = clarity.ToUpper() switch
            {
                "FL" => 3.1m,
                "IF" => 3m,
                "VVS1" => 2.4m,
                "VVS2" => 2.0m,
                "VS1" => 1.8m,
                "VS2" => 1.6m,
                "SI1" => 1.2m,
                "SI2" => 1.0m,
                _ => 1.0m
            };

            decimal cutPriceAdjustment = cut.ToLower() switch
            {
                "excellent" => 1.5m,
                "very good" => 1.3m,
                "good" => 1.2m,
                "fair" => 1.0m,
                _ => 1.0m
            };

            decimal finalPrice = caratPriceAdjustment * colorPriceAdjustment * clarityPriceAdjustment * cutPriceAdjustment;
            return finalPrice;
        }
    }
}
