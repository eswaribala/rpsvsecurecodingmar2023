using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankingApp.Contexts;
using BankingApp.Models;

namespace BankingApp.Pages_Corporates
{
    public class DeleteModel : PageModel
    {
        private readonly BankingApp.Contexts.CustomerContext _context;

        public DeleteModel(BankingApp.Contexts.CustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Corporate Corporate { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.Corporates == null)
            {
                return NotFound();
            }
            var corporate = await _context.Corporates.FindAsync(id);

            if (corporate != null)
            {
                Corporate = corporate;
                _context.Corporates.Remove(Corporate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
