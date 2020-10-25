using System;
namespace CodeSmells.Repositories
{
    public class Treshold
    {
        public string Level { get; }
        public decimal LimitBottom { get; }
        public double Discount { get; }

        public static bool isLevelValid(string level) =>
            (level == "standard" || level == "silver" || level == "gold");

        public Treshold(string level, decimal limitBottom, double discount)
        {
            if (!isLevelValid(level))
                throw new ArgumentException(
                    $"Invalid customer level (actual value:{level}" +
                    ", but expected one of: standard, silver, gold", "level");
            Level = level;
            LimitBottom = limitBottom;
            Discount = discount;
        }
    }

}
