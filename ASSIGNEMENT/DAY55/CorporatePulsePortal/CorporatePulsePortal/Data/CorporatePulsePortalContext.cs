using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CorporatePulsePortal.Models;

namespace CorporatePulsePortal.Data
{
    public class CorporatePulsePortalContext : DbContext
    {
        public CorporatePulsePortalContext (DbContextOptions<CorporatePulsePortalContext> options)
            : base(options)
        {
        }

        public DbSet<CorporatePulsePortal.Models.Employee> Employee { get; set; } = default!;
    }
}
