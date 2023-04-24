using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankingApp.Contexts;
using BankingApp.Models;

namespace BankingApp.Pages_Loans
{
    public class DeleteModel : PageModel
    {
        private readonly BankingApp.Contexts.CustomerContext _context;

        public DeleteModel(BankingApp.Contexts.CustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Loan Loan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Loans == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FirstOrDefaultAsync(m => m.ID == id);

            if (loan == null)
            {
                return NotFound();
            }
            else 
            {
                Loan = loan;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Loans == null)
            {
                return NotFound();
            }
            var loan = await _context.Loans.FindAsync(id);

            if (loan != null)
            {
                Loan = loan;
                _context.Loans.Remove(Loan);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
