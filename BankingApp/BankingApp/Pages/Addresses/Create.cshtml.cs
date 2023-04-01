using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomerApp.Contexts;
using CustomerApp.Models;

namespace CustomerApp.Pages.Addresses
{
    public class CreateModel : PageModel
    {
        private readonly CustomerApp.Contexts.BankingContext _context;

        public CreateModel(CustomerApp.Contexts.BankingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email");
        ViewData["CityId"] = new SelectList(_context.Cities, "Name", "Name");
            return Page();
        }


        [BindProperty]
        public Address Address { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Addresses.Add(Address);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
