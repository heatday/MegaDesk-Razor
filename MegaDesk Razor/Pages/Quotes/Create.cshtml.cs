using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDesk_Razor.Data;
using MegaDesk_Razor.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaDesk_Razor.Pages.Quotes
{
    public class CreateModel : PageModel
    {
        private readonly MegaDesk_Razor.Data.MegaDeskContext _context;

        public CreateModel(MegaDesk_Razor.Data.MegaDeskContext context)
        {
            _context = context;
        }

        public SelectList DeliveryTypes { get; set; }
        public SelectList Materials { get; set; }

        public async Task OnGet()
        {
            DeliveryTypes = new SelectList(await _context.DeliveryTypes.ToListAsync(), "Id", "Name");
            Materials = new SelectList(await _context.Materials.ToListAsync(), "Id", "Name");
        }

        [BindProperty]
        public Quote Quote { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Quotes == null || Quote == null)
            {
                return Page();
            }

            _context.Quotes.Add(Quote);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
