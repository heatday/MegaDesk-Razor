using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MegaDesk_Razor.Models;
using MegaDesk_Razor.Data;
using System.Collections.Generic;
using System.Linq;

namespace MegaDesk_Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MegaDeskContext _context;

        public IndexModel(ILogger<IndexModel> logger, MegaDeskContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<DeliveryType> DeliveryTypes { get; set; }
        public List<Material> Materials { get; set; }
        public Quote Quote { get; set; }

        public void OnGet()
        {
            DeliveryTypes = _context.DeliveryTypes.ToList();
            Materials = _context.Materials.ToList();
            Quote = new Quote();
        }
    }
}
