using System.Collections.Generic;
using MegaDesk_Razor.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaDesk_Razor.Data
{
    public class MegaDeskContext : DbContext
    {
        public MegaDeskContext(DbContextOptions<MegaDeskContext> options)
            : base(options)
        {
        }

        public DbSet<Quote> Quotes { get; set; }
    }
}
