using Microsoft.EntityFrameworkCore;
using InventoryManagement.Models;

namespace InventoryManagement.Data {
    public class InventoryManagementContext : DbContext {

        readonly string _connectionString = "Server=mysql-314910d5-yarmoshikmike-0c69.a.aivencloud.com;Port=20457;Database=InventoryManagement;User=avnadmin;Password=AVNS_IcjyYlTwhCAwbGmZs72;";
        public DbSet<Location> Location { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Return> Return { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Membership> Membership { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Inventory>().HasKey(e => new { e.LocationID, e.ItemID });
            modelBuilder.Entity<Review>().HasKey(e => new { e.CustomerID, e.ItemID });
            modelBuilder.Entity<OrderItem>().HasKey(e => new { e.OrderID, e.ItemID });
            modelBuilder.Entity<Cart>().HasKey(e => new { e.CustomerID, e.ItemID });
        }

    }
}
