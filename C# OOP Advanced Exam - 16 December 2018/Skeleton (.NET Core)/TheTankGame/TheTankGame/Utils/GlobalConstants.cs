namespace TheTankGame.Utils
{
    public class GlobalConstants
    {
        public static string VehicleSuccessMessage = "Created {0} Vehicle - {1}";

        public static string PartSuccessMessage = "Added {0} - {1} to Vehicle - {2}";

        public static string BattleSuccessMessage = "{0} versus {1} -> {2} Wins! Flawless Victory!";

               
        public const string ModelNameIsNullOrEmptyErrorMessage = "Model cannot be null or white space!";
        public const string WeightIsLessThanZeroOrZeroErrorMessage = "Weight cannot be less or equal to zero!";
        public const string PriceIsLessThanZeroOrZeroErrorMessage = "Price cannot be less or equal to zero!";
        public const string AttackIsLessThanZeroOrZeroErrorMessage = "Attack cannot be less than zero!";
        public const string DefenceIsLessThanZeroOrZeroErrorMessage = "Defense cannot be less than zero!";
        public const string HitPointsIsLessThanZeroOrZeroErrorMessage = "HitPoints cannot be less than zero!";

    }
}