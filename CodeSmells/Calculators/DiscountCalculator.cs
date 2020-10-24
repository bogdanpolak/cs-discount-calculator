using System;
using System.Collections.Generic;
using System.Linq;
using CodeSmells.Repositories;

namespace CodeSmells.Calculators
{
    public class DiscountCalculator
    {
        private OrderRepository _orderRepository;
        public TresholdRepository _tresholdRepository;
        public CustomerRepository _customerRepository;

        public DiscountCalculator(OrderRepository orderRepository,
            TresholdRepository tresholdRepository,
            CustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _tresholdRepository = tresholdRepository;
            _customerRepository = customerRepository;
        }

        private class Discount
        {
            public decimal RangeStart { get; }
            public decimal RangeEnd { get; }
            public decimal Value { get; }

            public Discount(decimal rangeStart, decimal rangeEnd, decimal value)
            {
                RangeStart = rangeStart;
                RangeEnd = rangeEnd;
                Value = value;
            }
        }

        const decimal MAX_PRICE = 999999999.00m;

        private List<Discount> discounts = new List<Discount>();

        private void GenerateDiscountTable(string level)
        {
            var rangeStart = 0m;
            var discount = 0m;
            var thresholds = _tresholdRepository.Get(level);
            foreach (var treshold in thresholds)
            {
                discounts.Add(new Discount(rangeStart, treshold.LimitBottom, discount));
                discount = (decimal)treshold.Discount;
                rangeStart = treshold.LimitBottom;
            }
            discounts.Add(new Discount(rangeStart, MAX_PRICE, discount));
        }

        public Decimal Calculate(int orderId)
        {
            var orderItems = _orderRepository.GetOrder(orderId).ToList();
            var customerId = orderItems.Select(c => c.CustomerId).FirstOrDefault();
            Customer customer = _customerRepository.GetCustomer(customerId);
            var totalBeforeDiscount = orderItems.Sum(oi => oi.UnitPrice * oi.Units);
            if (discounts.Count == 0)
                GenerateDiscountTable(customer.Level);
            var discountValue = discounts
                .Where(d => totalBeforeDiscount.InRange(d.RangeStart, d.RangeEnd))
                .Select(d => d.Value)
                .FirstOrDefault();
            var totalAfterDeduction = 0m;
            foreach (OrderItem item in orderItems)
            {
                if (discountValue > 0 && item.AllowsDeduction)
                {
                    item.Deduction = discountValue;
                    item.Total = item.UnitPrice * item.Units * (1 - item.Deduction);
                }
                else
                {
                    item.Total = item.UnitPrice * item.Units;
                }
                totalAfterDeduction += item.Total;
            }
            return totalAfterDeduction;
        }
    }
}
