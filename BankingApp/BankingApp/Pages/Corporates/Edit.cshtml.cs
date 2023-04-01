using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerApp.Contexts;
using CustomerApp.Models;

namespace CustomerApp.Pages.Corporates
{
    public class EditModel : PageModel
    {
        private readonly CustomerApp.Contexts.BankingContext _context;

        public EditModel(CustomerApp.Contexts.BankingContext context)
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

            var corporate =  await _context.Corporates.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (corporate == null)
            {
                return NotFound();
            }
            Corporate = corporate;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Corporate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorporateExists(Corporate.CustomerId))
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

        private bool CorporateExists(long id)
        {
          return _context.Corporates.Any(e => e.CustomerId == id);
        }
    }
}
