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

        public DbSet<CustomerEntity>Customers { get; set; }
        public DbSet<ProductEntity>Products { get; set; }
        public DbSet<TransactionEntity>Transactions{ get; set; }
    }
}