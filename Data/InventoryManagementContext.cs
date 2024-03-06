using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using InventoryManagement.Models;

namespace InventoryManagement.Data {
    public class InventoryManagementContext : DbContext {

        readonly string _connectionString = "";
        public DbSet<Location> Location { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Customer> Customer { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Inventory>().HasKey(e => new { e.LocationID, e.ItemID });
        }

    }
}
