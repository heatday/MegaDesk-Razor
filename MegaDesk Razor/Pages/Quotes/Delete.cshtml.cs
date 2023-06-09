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
    public class DeleteModel : PageModel
    {
        private readonly MegaDesk_Razor.Data.MegaDeskContext _context;

        public DeleteModel(MegaDesk_Razor.Data.MegaDeskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quote Quote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Quotes == null)
            {
                return NotFound();
            }

            var quote = await _context.Quotes
                .Include(q => q.DeliveryType)
                .Include(q => q.Material)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (quote == null)
            {
                return NotFound();
            }
            else
            {
                Quote = quote;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Quotes == null)
            {
                return NotFound();
            }
            var quote = await _context.Quotes.FindAsync(id);

            if (quote != null)
            {
                Quote = quote;
                _context.Quotes.Remove(Quote);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
