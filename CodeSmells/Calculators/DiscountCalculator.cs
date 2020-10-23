using System;
using System.Collections.Generic;
using System.Linq;
using CodeSmells.Repositories;

namespace CodeSmells.Calculators
{
    public class DiscountCalculator
    {
        private class Discount
        {
            public decimal RangeStart { get; }
            public decimal RangeEnd { get; }
            public decimal Value { get;  }

            public Discount(decimal rangeStart, decimal rangeEnd, decimal value)
            {
                RangeStart = rangeStart;
                RangeEnd = rangeEnd;
                Value = value;
            }
        }

        const decimal MAX_PRICE = 999999999.00m;

        private List<Discount> discounts = new List<Discount>();

        private void GenerateDiscountTable()
        {
            var rangeStart = 0m;
            var discount = 0m;
            Dictionary<decimal, double> thresholds = TresholdRepository.Get();
            foreach (var pair in thresholds)
            {
                discounts.Add(new Discount(rangeStart, pair.Key, discount));
                discount = (decimal)pair.Value;
                rangeStart = pair.Key;
            }
            discounts.Add(new Discount(rangeStart, MAX_PRICE, discount));
        }

        public Decimal Calculate(IEnumerable<OrderItem> orderItems)
        {
            var totalBeforeDiscount = orderItems.Sum(oi => oi.UnitPrice * oi.Units);
            if (discounts.Count == 0)
                GenerateDiscountTable();
            var discountValue = discounts
                .Where(d => totalBeforeDiscount.InRange(d.RangeStart, d.RangeEnd))
                .Select(d => d.Value)
                .FirstOrDefault();
            var totalAfterDeduction = 0m;
            foreach (OrderItem item in orderItems)
            {
                if (discountValue>0 && item.AllowsDeduction)
                {
                    item.Deduction = discountValue;
                    item.Total = item.UnitPrice * item.Units * (1-item.Deduction);
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
