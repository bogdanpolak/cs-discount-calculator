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
            new OrderItem(1, 25, "blue jeans", true, 100, 20),
            new OrderItem(1, 2, "transport service", false, 90, 1),
            new OrderItem(1, 99, "sport shoes", true, 120, 4),
            new OrderItem(2, 21, "violet jeans trousers", true, 100, 20),
            new OrderItem(2, 31, "men's jacket", true, 150, 3),
            new OrderItem(5, 21, "green socks pack", false, 20, 50),
            new OrderItem(5, 19, "winter boy coat", true, 170, 5),
            new OrderItem(5, 9, "red T-shirt with thunder ", false, 25, 32),
            new OrderItem(5, 5, "orange sweater", true, 140, 10),
            new OrderItem(6, 21, "sport shorts", false, 15, 10),
            new OrderItem(6, 19, "originals tracksuit", true, 145, 2),
            new OrderItem(6, 8, "boy outdoor-vest", true, 190, 1),
            new OrderItem(6, 11, "brown boots", true, 200, 2),
            new OrderItem(7, 12, "green cap", false, 35, 10),
            new OrderItem(7, 13, "bussines watch", false, 90, 2),
            new OrderItem(7, 7, "blue shawl", true, 19, 5),
            new OrderItem(7, 6, "navy wallet", true, 110, 4)
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

        public IEnumerable<OrderItem> LoadOrder(int orderId)
        {
            var order = orders.Where(o => o.OrderId == orderId);
            return order==null ? new List<OrderItem>() : order;
        }
    }
}
