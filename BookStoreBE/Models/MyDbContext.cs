using Microsoft.EntityFrameworkCore;

namespace BookStoreBE.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
                
        }
        public DbSet<Users> users { get; set; }
        public DbSet<Books> books { get; set; }
        public DbSet<Cart> cart { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<OrderItems> orderItems { get; set; }
    }
}
