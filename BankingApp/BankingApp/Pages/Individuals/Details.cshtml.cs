using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CustomerApp.Contexts;
using CustomerApp.Models;

namespace CustomerApp.Pages.Individuals
{
    public class DetailsModel : PageModel
    {
        private readonly CustomerApp.Contexts.BankingContext _context;

        public DetailsModel(CustomerApp.Contexts.BankingContext context)
        {
            _context = context;
        }

      public Individual Individual { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Individuals == null)
            {
                return NotFound();
            }

            var individual = await _context.Individuals.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (individual == null)
            {
                return NotFound();
            }
            else 
            {
                Individual = individual;
            }
            return Page();
        }
    }
}
