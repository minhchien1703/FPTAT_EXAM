using Example_C_2.Context;
using Example_C_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Example_C_2.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationContextDb _context;

        public CreateModel(ApplicationContextDb context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
