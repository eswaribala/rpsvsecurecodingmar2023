using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace BankingApp.Pages.Individuals
{
    public class EditModel : PageModel
    {
        private readonly CustomerContext _context;

        public EditModel(CustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Individual Individual { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Individuals == null)
            {
                return NotFound();
            }

            var individual =  await _context.Individuals.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (individual == null)
            {
                return NotFound();
            }
            Individual = individual;
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

            _context.Attach(Individual).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndividualExists(Individual.CustomerId))
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

        private bool IndividualExists(long id)
        {
          return _context.Individuals.Any(e => e.CustomerId == id);
        }
    }
}
