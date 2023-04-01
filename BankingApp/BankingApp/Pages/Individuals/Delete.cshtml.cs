using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace BankingApp.Pages.Individuals
{
    public class DeleteModel : PageModel
    {
        private readonly CustomerContext _context;

        public DeleteModel(CustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.Individuals == null)
            {
                return NotFound();
            }
            var individual = await _context.Individuals.FindAsync(id);

            if (individual != null)
            {
                Individual = individual;
                _context.Individuals.Remove(Individual);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
