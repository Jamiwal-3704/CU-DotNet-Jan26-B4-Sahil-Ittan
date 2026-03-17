using FinanceTrackerPro.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerPro.Data
{
    public class FinanceTrackerProContext : DbContext
    {
        public FinanceTrackerProContext(DbContextOptions<FinanceTrackerProContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }

        public DbSet<Transaction> Transaction { get; set; }
    }
}