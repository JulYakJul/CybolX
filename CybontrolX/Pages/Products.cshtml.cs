using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CybontrolX.DataBase;
using CybontrolX.DBModels;

namespace CybontrolX.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly AppDbContext _context;

        public ProductsModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = new List<Product>();

        [BindProperty]
        public List<int> SelectedProductIds { get; set; } = new List<int>();

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SortDescending { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                query = query.Where(p => p.Name.Contains(SearchQuery));
            }

            query = SortColumn switch
            {
                "Name" => SortDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "PurchasePrice" => SortDescending ? query.OrderByDescending(p => p.PurchasePrice) : query.OrderBy(p => p.PurchasePrice),
                "SalePrice" => SortDescending ? query.OrderByDescending(p => p.SalePrice) : query.OrderBy(p => p.SalePrice),
                "Quantity" => SortDescending ? query.OrderByDescending(p => p.Quantity) : query.OrderBy(p => p.Quantity),
                _ => query.OrderBy(p => p.Name),
            };

            Products = await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteProductsAsync()
        {
            if (SelectedProductIds.Any())
            {
                var productsToDelete = await _context.Products
                    .Where(p => SelectedProductIds.Contains(p.Id))
                    .ToListAsync();

                _context.Products.RemoveRange(productsToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
