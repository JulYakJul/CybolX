using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CybontrolX.DataBase;
using CybontrolX.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CybontrolX.Pages
{
    public class CreateProductModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateProductModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = new List<Product>();
        public string NotificationMessage { get; private set; } = string.Empty;

        public async Task OnGetAsync()
        {
            Products = await _context.Products.ToListAsync();
        }

        [BindProperty]
        public Product NewProduct { get; set; }

        public async Task<IActionResult> OnPostAddProductAsync()
        {
            if (!ModelState.IsValid)
            {
                NotificationMessage = "Ошибка: Некорректные данные.";
                return Page();
            }

            try
            {
                _context.Products.Add(NewProduct);
                await _context.SaveChangesAsync();
                NotificationMessage = "Товар успешно добавлен в список товаров.";
            }
            catch
            {
                NotificationMessage = "Ошибка: Товар не был добавлен.";
            }

            return Page();
        }
    }
}
