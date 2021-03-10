namespace BE
{
    public class OrderRow
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int productId { get; set; }
        public int amount { get; set; }

        public OrderRow(int orderId, int productId, int amount)
        {
            this.orderId = orderId;
            this.productId = productId;
            this.amount = amount;
        }

        public OrderRow(int productId, int amount)
        {            
            this.productId = productId;
            this.amount = amount;
        }
        public OrderRow() { }
    }
}
