using GeneralStore.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeneralStore_MVC_NET6.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer>Customers { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<Transaction>Transactions{ get; set; }
    }
}