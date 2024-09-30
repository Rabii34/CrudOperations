using Assignment_1.Models;
using Microsoft.EntityFrameworkCore;
namespace Assignment_1.Data
{
    public class ProductContext:DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Retailer> Retailers { get; set; }
    }
}
