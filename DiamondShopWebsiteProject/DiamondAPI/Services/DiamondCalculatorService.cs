namespace DiamondAPI.Services
{
    public class DiamondCalculatorService
    {
        public decimal CalculateDiamondPrice(double? carat, string? cut, string? color, string? clarity)
        {
            // Check for null and assign default values if necessary
            if (!carat.HasValue)
            {
                throw new ArgumentNullException(nameof(carat), "Carat cannot be null.");
            }
            // Typecast carat to decimal after ensuring it's not null
            decimal caratDecimal = (decimal)carat;

            // Provide default values or handle null for other parameters
            cut ??= "Good"; // Default to "Good" if null
            color ??= "I"; // Default to "I" if null
            clarity ??= "SI1"; // Default to "SI1" if null

            // Base price per carat. This is a simplified assumption.
            decimal basePrice = 2000m;

            // Adjust price based on carat weight
            decimal caratPriceAdjustment = caratDecimal * basePrice;

            // Adjust price based on cut quality
            decimal cutPriceAdjustment = cut switch
            {
                "Astor Ideal" => 1.7m,
                "Ideal" => 1.5m,
                "Very Good" => 1.2m,
                "Good" => 1.0m,
                _ => 1.0m // Default case if the cut is not recognized
            };

            // Adjust price based on color grade
            decimal colorPriceAdjustment = color switch
            {
                "D" => 1.5m,
                "E" => 1.4m,
                "F" => 1.3m,
                "G" => 1.2m,
                "H" => 1.1m,
                "I" => 1.0m,
                "J" => 0.9m,
                "K" => 0.8m,
                _ => 1.0m // Default case if the color is not recognized
            };

            // Adjust price based on clarity grade
            decimal clarityPriceAdjustment = clarity switch
            {
                "FL" => 1.5m,
                "IF" => 1.4m,
                "VVS1" => 1.3m,
                "VVS2" => 1.2m,
                "VS1" => 1.1m,
                "VS2" => 1.0m,
                "SI1" => 0.9m,
                "SI2" => 0.8m,
                _ => 1.0m // Default case if the clarity is not recognized
            };

            // Calculate the final price
            decimal finalPrice = caratPriceAdjustment * cutPriceAdjustment * colorPriceAdjustment * clarityPriceAdjustment;

            return finalPrice;
        }

    }
}
