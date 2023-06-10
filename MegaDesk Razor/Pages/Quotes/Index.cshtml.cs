using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public string SortBy { get; set; }
        public string SearchString { get; set; }

        public async Task OnGetAsync(string sortBy, string searchString)
        {
            SortBy = sortBy;
            SearchString = searchString;

            var quotesQuery = _context.Quotes
                .Include(q => q.DeliveryType)
                .Include(q => q.Material)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                quotesQuery = quotesQuery.Where(q => q.CustomerName.Contains(searchString));
            }

            switch (sortBy)
            {
                case "Date":
                    quotesQuery = quotesQuery.OrderBy(q => q.Date);
                    break;
                case "CustomerName":
                    quotesQuery = quotesQuery.OrderBy(q => q.CustomerName);
                    break;
                default:
                    quotesQuery = quotesQuery.OrderByDescending(q => q.Id); // Sort by ID by default
                    break;
            }

            Quote = await quotesQuery.ToListAsync();
        }
    }
}





