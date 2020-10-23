using System.Collections.Generic;

namespace CodeSmells.Repositories
{
    public class TresholdRepository {
        static private Dictionary<decimal, double> thresholds = new Dictionary<decimal, double>()
        {
            { 1000.00m, 0.03d },
            { 1400.00m, 0.05d },
            { 1900.00m, 0.08d }
        };

        static public Dictionary<decimal, double> Get() => thresholds;
    }
}
