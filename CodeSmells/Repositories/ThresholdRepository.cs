using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmells.Repositories
{
    public class Treshold {
        public string Level { get; }
        public decimal LimitBottom { get; }
        public double Discount { get; }

        public static bool isLevelValid (string level) => 
            (level == "standard" || level == "silver" || level == "gold");

        public Treshold(string level, decimal limitBottom, double discount)
        {
            if ( !isLevelValid(level) )
                throw new ArgumentException(
                    $"Invalid customer level (actual value:{level}"+
                    ", but expected one of: standard, silver, gold", "level");
            Level = level;
            LimitBottom = limitBottom;
            Discount = discount;
        }
    }

    public class TresholdRepository {
        private List<Treshold> thresholds = new List<Treshold>()
        {
            // standard
            new Treshold( "standard", 1000.00m, 0.02 ),
            new Treshold( "standard", 2000.00m, 0.03 ),
            new Treshold( "standard", 3000.00m, 0.04 ),
            // silver
            new Treshold( "silver", 800.00m, 0.02 ),
            new Treshold( "silver", 1500.00m, 0.03 ),
            new Treshold( "silver", 2000.00m, 0.05 ),
            // gold
            new Treshold( "gold",  700.00m, 0.02 ),
            new Treshold( "gold", 1000.00m, 0.03 ),
            new Treshold( "gold", 1400.00m, 0.05 ),
            new Treshold( "gold", 1900.00m, 0.08 )
        };

        public IList<Treshold> Get(string level) => thresholds
            .Where( tr => tr.Level == level )
            .OrderBy ( tr => tr.LimitBottom )
            .ToList();
    }
}
