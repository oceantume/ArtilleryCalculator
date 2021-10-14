namespace ArtilleryCalculator
{
    class RusDistanceElevationConverter : IDistanceElevationConverter
    {
        private const decimal A = 1120m;
        private const decimal B = 21.33m;
        private const decimal L = 100m;

        public decimal ConvertDistanceToElevation(decimal distance)
        {
            return A - (((distance / L) - 1) * B);
        }
    }
}
