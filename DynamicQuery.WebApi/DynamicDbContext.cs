using Microsoft.EntityFrameworkCore;

namespace DynamicQuery.WebApi
{
    public class Order
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public Customer? Customer { get; set; }
        public List<OrderLine>? Lines { get; set; }
    }

    public class Customer
    {
        public string? Id { get; set; }
    }

    public class OrderLine
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }

    public class DynamicDbContext : DbContext
    {
        public DynamicDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}