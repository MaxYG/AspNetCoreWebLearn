using System.Threading.Tasks;
using AspNetCoreData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreWebDemo.Pages.Customer
{
    public class CreateModel : PageModel
    {
        private readonly AspCoreDbContext _db;
        public CreateModel(AspCoreDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public AspNetCoreData.Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Customers.Add(Customer);
            await _db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        public void OnGet()
        {
        }
    }
}