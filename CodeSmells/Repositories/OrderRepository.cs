using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmells.Repositories
{
    public class OrderRepository
    {
        public OrderRepository()
        {
        }

        private List<OrderItem> orders = new List<OrderItem>()
        {
            new OrderItem(1, "PL3815422868", 25, "blue jeans", true, 100, 20),
            new OrderItem(1, "PL3815422868", 2, "transport service", false, 90, 1),
            new OrderItem(1, "PL3815422868", 99, "sport shoes", true, 120, 4),
            new OrderItem(2, "DE136695976", 21, "violet jeans trousers", true, 100, 20),
            new OrderItem(2, "DE136695976", 31, "men's jacket", true, 150, 3),
            new OrderItem(5, "PL1124267312", 21, "green socks pack", false, 20, 50),
            new OrderItem(5, "PL1124267312", 19, "winter boy coat", true, 170, 5),
            new OrderItem(5, "PL1124267312", 9, "red T-shirt with thunder ", false, 25, 32),
            new OrderItem(5, "PL1124267312", 5, "orange sweater", true, 140, 10),
            new OrderItem(6, "DE301204526", 21, "sport shorts", false, 15, 10),
            new OrderItem(6, "DE301204526", 19, "originals tracksuit", true, 145, 2),
            new OrderItem(6, "DE301204526", 8, "boy outdoor-vest", true, 190, 1),
            new OrderItem(6, "DE301204526", 11, "brown boots", true, 200, 2),
            new OrderItem(7, "PL5352679105", 12, "green cap", false, 35, 10),
            new OrderItem(7, "PL5352679105", 13, "bussines watch", false, 90, 2),
            new OrderItem(7, "PL5352679105", 7, "blue shawl", true, 19, 5),
            new OrderItem(7, "PL5352679105", 6, "navy wallet", true, 110, 4)
        };

        public int AddOrder(List<OrderItem> orderitems)
        {
            var orderid = orders.Max(o => o.OrderId) + 1;
            foreach (var orderitem in orderitems)
            {
                orderitem.OrderId = orderid;
                orders.Add(orderitem);
            }
            return orderid; 
        }

        public void RemoveOrder(int orderid)
        {
            var orderitem = orders.Where(o => o.OrderId == orderid).FirstOrDefault();
            if (orderitem != null)
                orders.Remove(orderitem);
        }

        public IEnumerable<int> GetOrderIds(Period period)
        {
            if (period.Start.Year < 2000)
                throw new ArgumentException("Invalid parametr value", "period");
            return orders.Select(oi => oi.OrderId).Distinct();
        }

        public IEnumerable<OrderItem> GetOrder(int orderId)
        {
            var order = orders.Where(o => o.OrderId == orderId);
            return order==null ? new List<OrderItem>() : order;
        }
    }
}
