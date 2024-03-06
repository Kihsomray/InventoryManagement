using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using InventoryManagement.Models;

namespace InventoryManagement.Data
{
    public class InventoryManagementContext : DbContext
    {

        readonly string _connectionString = "";
        public DbSet<Location> Location { get; set; }
        public DbSet<Employee> Employee { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }

        // Other configurations, if needed
    }
}
