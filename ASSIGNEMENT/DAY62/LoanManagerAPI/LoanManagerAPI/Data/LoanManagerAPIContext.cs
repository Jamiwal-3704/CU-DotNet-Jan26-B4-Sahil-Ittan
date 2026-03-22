using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoanManagerAPI.Models;

namespace LoanManagerAPI.Data
{
    public class LoanManagerAPIContext : DbContext
    {
        public LoanManagerAPIContext (DbContextOptions<LoanManagerAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Loan> Loan { get; set; } = default!;
    }
}
