using System;
using System.Collections.Generic;

namespace CodeSmells.Repositories
{
    public class CustomerRepository
    {
        public CustomerRepository()
        {
        }

        private List<Customer> customers = new List<Customer>
        {
            new Customer("PL3815422868","Wieczorek sp. z o.o.","gold"),
            new Customer("PL5352679105","Fundacja Michalak","silver"),
            new Customer("DE136695976","Haag GmbH","standard"),
            new Customer("DE301204526","Haag GmbH","gold")
        };
    }
}
