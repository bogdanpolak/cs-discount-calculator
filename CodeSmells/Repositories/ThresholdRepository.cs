using System.Collections.Generic;
using System.Linq;

namespace CodeSmells.Repositories
{
    public enum CustomerLevel {
        standard,
        silver,
        gold
    };

    public class Treshold {
        public CustomerLevel Level { get; }
        public decimal LimitBottom { get; }
        public double Discount { get; }

        public Treshold(CustomerLevel level, decimal limitBottom, double discount)
        {
            Level = level;
            LimitBottom = limitBottom;
            Discount = discount;
        }
    }

    public class TresholdRepository {
        private List<Treshold> thresholds = new List<Treshold>()
        {
            // standard
            new Treshold( CustomerLevel.standard, 1000.00m, 0.02 ),
            new Treshold( CustomerLevel.standard, 2000.00m, 0.03 ),
            new Treshold( CustomerLevel.standard, 3000.00m, 0.04 ),
            // silver
            new Treshold( CustomerLevel.silver, 900.00m, 0.03 ),
            new Treshold( CustomerLevel.silver, 1500.00m, 0.04 ),
            new Treshold( CustomerLevel.silver, 2000.00m, 0.06 ),
            // gold
            new Treshold( CustomerLevel.gold,  700.00m, 0.02 ),
            new Treshold( CustomerLevel.gold, 1000.00m, 0.03 ),
            new Treshold( CustomerLevel.gold, 1400.00m, 0.05 ),
            new Treshold( CustomerLevel.gold, 1900.00m, 0.08 )
        };

        public IList<Treshold> Get(CustomerLevel level) => thresholds
            .Where( tr => tr.Level == level )
            .OrderBy ( tr => tr.LimitBottom )
            .ToList();
    }
}
