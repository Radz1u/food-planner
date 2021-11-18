
using FoodPlanner.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.Infrastructure.DbContexts
{
    public class EFDbContext:DbContext
    {
        public EFDbContext()
        {
        }

        public EFDbContext(DbContextOptions<EFDbContext> options)
        :base(options)
        {
            
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ReceiptEntity> Receipts { get; set; }
    }
}
