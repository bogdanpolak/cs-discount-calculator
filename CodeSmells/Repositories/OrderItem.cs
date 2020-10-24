namespace CodeSmells.Repositories
{
    public class OrderItem
    {
        public int OrderId { get; }
        public int ProductId { get; }
        public string ProductName { get; }
        public bool AllowsDeduction { get; set; }
        public decimal UnitPrice { get; }
        public int Units { get; set; }
        public decimal Deduction { get; set; }
        public decimal Total { get; set; }

        public OrderItem(int orderId, int productId, string productName,
            bool allowsDeduction, decimal unitPrice, int units)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            AllowsDeduction = allowsDeduction;
            UnitPrice = unitPrice;
            Units = units;
            Deduction = 0;
            Total = unitPrice * units;
        }
    }
}
