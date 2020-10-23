using System.Collections.Generic;

namespace CodeSmells.Repositories
{
    public class Treshold {
        public decimal LimitBottom { get; }
        public double Discount { get; }

        public Treshold(decimal limitBottom, double discount)
        {
            LimitBottom = limitBottom;
            Discount = discount;
        }
    }

    public class TresholdRepository {
        private List<Treshold> thresholds = new List<Treshold>()
        {
            new Treshold( 1000.00m, 0.03 ),
            new Treshold( 1400.00m, 0.05 ),
            new Treshold( 1900.00m, 0.08 )
        };

        public List<Treshold> Get() => thresholds;
    }
}
