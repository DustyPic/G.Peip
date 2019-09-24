using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CreateIGuess.Data
{
    public class LasteDbContext : DbContext
    {
        public LasteDbContext(DbContextOptions<LasteDbContext> options)
            : base(options)
        {
        }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
    }
}