using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankingApp.Contexts;
using BankingApp.Models;

namespace BankingApp.Pages_Individuals
{
    public class CreateModel : PageModel
    {
        private readonly BankingApp.Contexts.CustomerContext _context;
        public List<SelectListItem> GenderList { get; set; }
        public CreateModel(BankingApp.Contexts.CustomerContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "MALE", Value = "MALE" });
            list.Add(new SelectListItem() { Text = "FEMALE", Value = "FEMALE" });
            list.Add(new SelectListItem() { Text = "TRANSGENDER", Value = "TRANSGENDER" });
            this.GenderList = list;
            return Page();
        }
        

        [BindProperty]
        public Individual Individual { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Individuals == null || Individual == null)
            {
                return Page();
            }

            _context.Individuals.Add(Individual);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
