using System;
namespace CodeSmells.Repositories
{
    public class Customer
    {
        public Customer(string vatID, string name, string level)
        {
            VatID = vatID;
            Name = name;
            Level = level;
        }

        public string VatID { get;  }
        public string Name { get;  }
        public string Level { get; }
    }
}
