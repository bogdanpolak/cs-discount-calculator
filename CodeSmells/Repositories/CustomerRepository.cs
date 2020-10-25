using System;
using System.Collections.Generic;
using System.Linq;

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

        public void AddCustomer(Customer customer)
        {
            if (!Threshold.IsLevelValid(customer.Level))
                throw new ArgumentException(
                    Threshold.GenInvalidLevelMsg(customer.Level), "customer");
            if (customers.Exists(c => c.VatID == customer.VatID))
                throw new ArgumentException(
                    $"Customer with id={customer.VatID} already exists",
                    "customer");
            customers.Add(customer);
        }

        public void RemoveCustomer (string customerid)
        {
            customers.Remove(customers.First(c => c.VatID == customerid));
        }

        public Customer GetCustomer(string customerid) =>
            customers.Where(c => c.VatID == customerid).FirstOrDefault();
    }
}
