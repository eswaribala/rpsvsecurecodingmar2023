﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace BankingApp.Pages.Addresses
{
    public class EditModel : PageModel
    {
        private readonly CustomerContext _context;

        public EditModel(CustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Address Address { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address =  await _context.Addresses.FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }
            Address = address;
           ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email");
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

            _context.Attach(Address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(Address.AddressId))
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

        private bool AddressExists(long id)
        {
          return _context.Addresses.Any(e => e.AddressId == id);
        }
    }
}
