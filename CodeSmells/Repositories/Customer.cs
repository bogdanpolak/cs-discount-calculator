using System;
namespace CodeSmells.Repositories
{
    public class Customer
    {
        public Customer(string vatID, string name, string level)
        {
            if (!Threshold.isLevelValid(level))
                throw new ArgumentException(
                    $"Invalid customer level (actual value:{level}" +
                    ", but expected one of: standard, silver, gold", "level");
            VatID = vatID;
            Name = name;
            Level = level;
        }

        public string VatID { get;  }
        public string Name { get;  }
        public string Level { get; }
    }
}
