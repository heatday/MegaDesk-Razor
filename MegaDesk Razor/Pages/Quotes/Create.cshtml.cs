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

        public  CreateModel(MegaDesk_Razor.Data.MegaDeskContext context)
        {
            _context = context;
        }

        public SelectList DeliveryTypes { get; set; }
        public SelectList Materials { get; set; }

        public async Task OnGet()
        {
            var deliveryTypes = await _context.DeliveryTypes.ToListAsync();
            DeliveryTypes = new SelectList(deliveryTypes, "Id", "Type");

            var materials = await _context.Materials.ToListAsync();
            Materials = new SelectList(materials, "Id", "Name");

        }


        [BindProperty]
        public Quote Quote { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                DeliveryTypes = new SelectList(await _context.DeliveryTypes.ToListAsync(), "Id", "Type");
                Materials = new SelectList(await _context.Materials.ToListAsync(), "Id", "Name");
                return Page();
            }

            Quote.DeliveryType = await _context.DeliveryTypes.FindAsync(Quote.DeliveryTypeId);

            if (Quote.DeliveryType == null)
            {
                // Handle the case when the DeliveryType is not found
                ModelState.AddModelError(string.Empty, "Invalid Delivery Type");
                DeliveryTypes = new SelectList(await _context.DeliveryTypes.ToListAsync(), "Id", "Type");
                Materials = new SelectList(await _context.Materials.ToListAsync(), "Id", "Name");
                return Page();
            }

            _context.Quotes.Add(Quote);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }



    }
}