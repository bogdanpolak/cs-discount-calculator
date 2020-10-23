namespace CodeSmells.Calculators
{
    public class OrderItem
    {
        public int ProductId { get; }
        public string ProductName { get; }
        public bool AllowsDeduction { get; set; }
        public decimal UnitPrice { get; }
        public int Units { get; set; }
        public decimal Deduction { get; set; }
        public decimal Total { get; set; }

        public OrderItem(int productId, string productName,
            bool allowsDeduction, decimal unitPrice, int units)
        {
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
