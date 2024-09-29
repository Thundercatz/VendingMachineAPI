using Microsoft.EntityFrameworkCore;
using VendingMachineAPI.Models;

namespace VendingMachineAPI.Data
{
    public class VendingMachineDbContext : DbContext
    {
        public VendingMachineDbContext(DbContextOptions<VendingMachineDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
