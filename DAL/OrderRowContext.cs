using BE;
using System.Data.Entity;

namespace DAL
{
    public class OrderRowContext : DbContext
    {
        public DbSet<OrderRow> RowOfOrders { get; set; }

        public OrderRowContext() : base("Row_Of_Orders") { }
    }
}
