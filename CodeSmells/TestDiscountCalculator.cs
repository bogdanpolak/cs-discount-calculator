using System;
using System.Collections.Generic;
using Xunit;
using CodeSmells.Calculators;
using CodeSmells.Repositories;

namespace CodeSmells
{
    public class Gloabal
    {
        static public TresholdRepository tresholdRepository = new TresholdRepository();
        static public OrderRepository orderRepository = new OrderRepository();
    }

    public class TestDiscountCalculator
    {
        private DiscountCalculator calculator;

        public TestDiscountCalculator() {
            calculator = new DiscountCalculator(Gloabal.orderRepository,
                Gloabal.tresholdRepository);
        }

        [Fact]
        public void Calculate_WithInvalidCustomerLevel()
        {
            var orderid = Gloabal.orderRepository.AddOrder ( new List<OrderItem>() {
                new OrderItem(-1, "PL5664390716", 25, "blue jeans trousers", true, 100, 2)
            });
            Assert.Throws<ArgumentException>( () => calculator.Calculate("invalid", orderid) );
            Gloabal.orderRepository.RemoveOrder(orderid);          
        }

        [Fact]
        public void Calculate_WithOneItem_WhenTotalBelowDiscount()
        {
            var orderid = Gloabal.orderRepository.AddOrder(new List<OrderItem>() {
                new OrderItem(-1, "PL5664390716", 25, "blue jeans trousers", true, 100, 2)
            });
            var actual = calculator.Calculate("gold", orderid);
            Assert.Equal(200.00m, actual);
            Gloabal.orderRepository.RemoveOrder(orderid);
        }

        [Fact]
        public void Calculate_WithOneItem_WhenTotalIsAboveFirstTreshold()
        {
            var orderid = Gloabal.orderRepository.AddOrder(new List<OrderItem>() {
                new OrderItem(-1, "PL5664390716", 25, "blue jeans trousers", true, 100, 11)
            });
            var actual = calculator.Calculate("gold", orderid);
            // 1100 * 0.3 = 1067
            Assert.Equal(1067.00m, actual);
        }

        [Fact]
        public void Calculate_WithDiscount_OneItemNotDeducted()
        {
            var actual = calculator.Calculate("gold", 1);
            Assert.Equal(2371.60m, actual);
        }
    }
}
