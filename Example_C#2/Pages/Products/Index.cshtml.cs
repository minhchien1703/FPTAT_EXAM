using Example_C_2.Context;
using Example_C_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Example_C_2.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContextDb _context;

        public IndexModel(ApplicationContextDb context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Products.ToListAsync();
        }
    }
}
