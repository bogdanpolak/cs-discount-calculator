using System;
using System.Collections.Generic;
using Xunit;
using CodeSmells.Calculators;
using CodeSmells.Repositories;

namespace CodeSmells
{
    public class TestDiscountCalculator
    {
        public DiscountCalculator calculator;

        public TestDiscountCalculator() {
            calculator = new DiscountCalculator();
        }

        [Fact]
        public void Calculate_WithInvalidCustomerLevel()
        {
            var orders = new List<OrderItem>() {
                new OrderItem(25, "blue jeans trousers", true, 100, 2)
            };
            Assert.Throws<ArgumentException>( () => calculator.Calculate("invalid", orders) );
        }

        [Fact]
        public void Calculate_WithOneItem_WhenTotalBelowDiscount()
        {
            var orders = new List<OrderItem>() {
                new OrderItem(25, "blue jeans trousers", true, 100, 2)
            };
            var actual = calculator.Calculate("gold", orders);
            Assert.Equal(200.00m, actual);
        }

        [Fact]
        public void Calculate_WithOneItem_WhenTotalIsAboveFirstTreshold()
        {
            var orders = new List<OrderItem>() {
                new OrderItem(25, "blue jeans trousers", true, 100, 11)
            };
            var actual = calculator.Calculate("gold", orders);
            // 1100 * 0.3 = 1067
            Assert.Equal(1067.00m, actual);
        }

        [Fact]
        public void Calculate_WithDiscount_OneItemNotDeducted()
        {
            var orders = new List<OrderItem>() {
                new OrderItem(25, "blue jeans trousers", true, 100, 20),
                new OrderItem(2, "transport service", false, 90, 1),
                new OrderItem(99, "sport shoes", true, 120, 4)
            };
            // total before: 2570 = 2000 + 90 + 480
            // discount: 8%
            // 2000*0.92 + 90 + 480*0.92 = 1840 + 90 + 441,60 = 2371.60
            var actual = new DiscountCalculator().Calculate("gold", orders);
            Assert.Equal(2371.60m, actual);
        }
    }
}
