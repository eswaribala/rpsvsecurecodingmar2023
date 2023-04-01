using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomerApp.Contexts;
using CustomerApp.Models;

namespace CustomerApp.Pages.Individuals
{
    public class CreateModel : PageModel
    {
        private readonly CustomerApp.Contexts.BankingContext _context;

        public List<SelectListItem> GenderList { get; set; }
        public CreateModel(CustomerApp.Contexts.BankingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text="MALE",Value="MALE"});
            list.Add(new SelectListItem() { Text = "FEMALE", Value = "FEMALE" });
            list.Add(new SelectListItem() { Text = "TRANSGENDER", Value = "TRANSGENDER" });
            this.GenderList = list;
            return Page();
        }

        [BindProperty]
        public Individual Individual { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Individuals.Add(Individual);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
