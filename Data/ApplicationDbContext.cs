using EmployeeManagement.Api.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Promtech_Machine_Test.Models;

namespace EmployeeManagement.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet represents the Employees table in the database
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }
    }
}