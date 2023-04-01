using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CustomerApp.Contexts;
using CustomerApp.Models;

namespace CustomerApp.Pages.Corporates
{
    public class DetailsModel : PageModel
    {
        private readonly CustomerApp.Contexts.BankingContext _context;

        public DetailsModel(CustomerApp.Contexts.BankingContext context)
        {
            _context = context;
        }

      public Corporate Corporate { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Corporates == null)
            {
                return NotFound();
            }

            var corporate = await _context.Corporates.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (corporate == null)
            {
                return NotFound();
            }
            else 
            {
                Corporate = corporate;
            }
            return Page();
        }
    }
}
