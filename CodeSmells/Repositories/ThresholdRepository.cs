using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmells.Repositories
{
    public class TresholdRepository {
        private List<Threshold> thresholds = new List<Threshold>()
        {
            // standard
            new Threshold( "standard", 1000.00m, 0.02 ),
            new Threshold( "standard", 2000.00m, 0.03 ),
            new Threshold( "standard", 3000.00m, 0.04 ),
            // silver
            new Threshold( "silver", 800.00m, 0.02 ),
            new Threshold( "silver", 1500.00m, 0.03 ),
            new Threshold( "silver", 2000.00m, 0.05 ),
            // gold
            new Threshold( "gold",  700.00m, 0.02 ),
            new Threshold( "gold", 1000.00m, 0.03 ),
            new Threshold( "gold", 1400.00m, 0.05 ),
            new Threshold( "gold", 1900.00m, 0.08 )
        };

        public IList<Threshold> Get(string level) => thresholds
            .Where( tr => tr.Level == level )
            .OrderBy ( tr => tr.LimitBottom )
            .ToList();
    }
}
