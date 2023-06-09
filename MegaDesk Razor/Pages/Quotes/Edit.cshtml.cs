using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaDesk_Razor.Data;
using MegaDesk_Razor.Models;

namespace MegaDesk_Razor.Pages.Quotes
{
    public class EditModel : PageModel
    {
        private readonly MegaDesk_Razor.Data.MegaDeskContext _context;

        public EditModel(MegaDesk_Razor.Data.MegaDeskContext context)
        {
            _context = context;
        }

        public SelectList DeliveryTypes { get; set; }
        public SelectList Materials { get; set; }

        [BindProperty]
        public Quote Quote { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Quotes == null)
            {
                return NotFound();
            }

            var quote = await _context.Quotes
                .Include(q => q.DeliveryType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (quote == null)
            {
                return NotFound();
            }

            Quote = quote;

            var deliveryTypes = await _context.DeliveryTypes.ToListAsync();
            DeliveryTypes = new SelectList(deliveryTypes, "Id", "Type", Quote.DeliveryType);

            var materials = await _context.Materials.ToListAsync();
            Materials = new SelectList(materials, "Id", "Name", Quote.Material);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var deliveryTypes = await _context.DeliveryTypes.ToListAsync();
                DeliveryTypes = new SelectList(deliveryTypes, "Id", "Type", Quote.DeliveryType);

                var materials = await _context.Materials.ToListAsync();
                Materials = new SelectList(materials, "Id", "Name", Quote.Material);

                return Page();
            }

            Quote.DeliveryType = await _context.DeliveryTypes.FindAsync(Quote.DeliveryTypeId);

            if (Quote.DeliveryType == null)
            {
                // Handle the case when the DeliveryType is not found
                ModelState.AddModelError(string.Empty, "Invalid Delivery Type");

                var deliveryTypes = await _context.DeliveryTypes.ToListAsync();
                DeliveryTypes = new SelectList(deliveryTypes, "Id", "Type", Quote.DeliveryType);

                var materials = await _context.Materials.ToListAsync();
                Materials = new SelectList(materials, "Id", "Name", Quote.Material);

                return Page();
            }

            _context.Attach(Quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteExists(Quote.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool QuoteExists(int id)
        {
            return _context.Quotes.Any(e => e.Id == id);
        }
    }
}
