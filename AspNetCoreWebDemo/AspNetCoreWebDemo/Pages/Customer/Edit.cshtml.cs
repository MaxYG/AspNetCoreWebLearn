using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebDemo.Pages.Customer
{
    public class EditModel : PageModel
    {
        private readonly AspCoreDbContext _db;
        public EditModel(AspCoreDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public AspNetCoreData.Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer =await _db.Customers.FindAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(Customer).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"Customer {Customer.Id} not found!");
            }

            return RedirectToPage("./Index");
        }
    }
}