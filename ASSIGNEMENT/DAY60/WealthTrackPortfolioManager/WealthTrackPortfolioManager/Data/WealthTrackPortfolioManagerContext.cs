using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WealthTrackPortfolioManager.Models;

namespace WealthTrackPortfolioManager.Data
{
    public class WealthTrackPortfolioManagerContext : DbContext
    {
        public WealthTrackPortfolioManagerContext (DbContextOptions<WealthTrackPortfolioManagerContext> options)
            : base(options)
        {
        }

        public DbSet<WealthTrackPortfolioManager.Models.Investment> Investment { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Investment>()
                .Property(i => i.PurchasePrice)
                .HasPrecision(18, 2);
        }
    }
}
