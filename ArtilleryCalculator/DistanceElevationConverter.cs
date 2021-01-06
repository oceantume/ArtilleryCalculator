namespace ArtilleryCalculator
{
    class DistanceElevationConverter
    {
        private const decimal A = 1001.465m;
        private const decimal B = -0.2371m;

        public decimal ConvertDistanceToElevation(decimal distance)
        {
            return A + (B * distance);
        }

        public decimal ConvertElevationToDistance(decimal elevation)
        {
            return (elevation - A) / B;
        }
    }
}
