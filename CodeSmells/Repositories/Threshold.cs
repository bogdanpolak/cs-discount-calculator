using System;
namespace CodeSmells.Repositories
{
    public class Threshold
    {
        public string Level { get; }
        public decimal LimitBottom { get; }
        public double Discount { get; }

        public static bool IsLevelValid(string level) =>
            (level == "standard" || level == "silver" || level == "gold");

        public static string GenInvalidLevelMsg(string currentLevel) =>
             $"Invalid customer level (actual value:{currentLevel}, but " +
             "expected one of following: standard, silver, gold";

        public Threshold(string level, decimal limitBottom, double discount)
        {
            if (!IsLevelValid(level))
                throw new ArgumentException(GenInvalidLevelMsg(level), "level");
            Level = level;
            LimitBottom = limitBottom;
            Discount = discount;
        }
    }

}
