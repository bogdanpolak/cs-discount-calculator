using System;
using System.Collections.Generic;
using Xunit;
using CodeSmells.Calculators;
using CodeSmells.Repositories;

namespace CodeSmells
{
    public class Globals
    {
        static public TresholdRepository tresholdRepository = new TresholdRepository();
        static public OrderRepository orderRepository = new OrderRepository();
        static public CustomerRepository customerRepository = new CustomerRepository();
    }

    public class TestDiscountCalculator
    {
        private DiscountCalculator calculator;

        public TestDiscountCalculator() {
            calculator = new DiscountCalculator(Globals.orderRepository,
                Globals.tresholdRepository, Globals.customerRepository);
        }

        [Fact]
        public void CareateCustomer_WithInvalidCustomerLevel()
        {
            Assert.Throws<ArgumentException>( () =>
                new Customer("PL5664390716", "Abc Sp.z o.o.", "invalid"));
        }

        [Fact]
        public void Calculate_WithOneItem_WhenTotalBelowDiscount()
        {
            var orderid = Globals.orderRepository.AddOrder(new List<OrderItem>() {
                new OrderItem(-1, "PL54321", 25, "blue jeans trousers", true, 100, 2)
            });
            var customer = new Customer("PL54321", "Test S.A.", "gold");
            Globals.customerRepository.AddCustomer(customer);
            // "gold"
            var actual = calculator.Calculate(orderid);
            Assert.Equal(200.00m, actual);
            Assert.Equal(200.00m, actual);
            Globals.orderRepository.RemoveOrder(orderid);
            Globals.customerRepository.RemoveCustomer(customer.VatID);
        }

        [Fact]
        public void Calculate_WithOneItem_WhenTotalIsAboveFirstTreshold()
        {
            var orderid = Globals.orderRepository.AddOrder(new List<OrderItem>() {
                new OrderItem(-1, "PL12345", 25, "blue jeans trousers", true, 100, 11)
            });
            var customer = new Customer("PL12345", "Test S.A.", "gold");
            Globals.customerRepository.AddCustomer(customer);
            var actual = calculator.Calculate(orderid);
            // 1100 * 0.3 = 1067
            Assert.Equal(1067.00m, actual);
            Globals.orderRepository.RemoveOrder(orderid);
            Globals.customerRepository.RemoveCustomer(customer.VatID);
        }

        [Fact]
        public void Calculate_WithDiscount_OneItemNotDeducted()
        {
            var actual = calculator.Calculate(1);
            Assert.Equal(2371.60m, actual);
        }
    }
}
