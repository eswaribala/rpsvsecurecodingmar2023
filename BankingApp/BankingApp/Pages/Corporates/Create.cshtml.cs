using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankingApp.Contexts;
using BankingApp.Models;

namespace BankingApp.Pages_Corporates
{
    public class CreateModel : PageModel
    {
        private readonly BankingApp.Contexts.CustomerContext _context;

        public CreateModel(BankingApp.Contexts.CustomerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Corporate Corporate { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Corporates == null || Corporate == null)
            {
                return Page();
            }

            _context.Corporates.Add(Corporate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
