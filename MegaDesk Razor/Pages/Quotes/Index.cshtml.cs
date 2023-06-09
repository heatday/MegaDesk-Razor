using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk_Razor.Data;
using MegaDesk_Razor.Models;

namespace MegaDesk_Razor.Pages.Quotes
{
    public class IndexModel : PageModel
    {
        private readonly MegaDesk_Razor.Data.MegaDeskContext _context;

        public IndexModel(MegaDesk_Razor.Data.MegaDeskContext context)
        {
            _context = context;
        }

        public IList<Quote> Quote { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            var quotesQuery = _context.Quotes
                .Include(q => q.DeliveryType) // Include the DeliveryType property
                .Include(q => q.Material) // Include the Material property
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                quotesQuery = quotesQuery.Where(q => q.CustomerName.Contains(searchString));
            }

            Quote = await quotesQuery.ToListAsync();
        }
    }

}
    

