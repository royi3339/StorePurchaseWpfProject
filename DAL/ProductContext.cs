using BE;
using System.Data.Entity;

namespace DAL
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> products { get; set; }
        public ProductContext() : base("ProductCatalog") { }
    }
}
