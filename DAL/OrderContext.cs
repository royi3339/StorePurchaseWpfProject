using BE;
using System.Data.Entity;

namespace DAL
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> orders { get; set; }
        public OrderContext() : base("Orders") { }
    }
}
