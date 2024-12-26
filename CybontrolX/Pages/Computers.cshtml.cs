using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CybontrolX.DataBase;
using CybontrolX.DBModels;

namespace CybontrolX.Pages
{
    public class ComputersModel : PageModel
    {
        private readonly AppDbContext _context;

        public ComputersModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Computer> Computers { get; set; } = new List<Computer>();

        [BindProperty]
        public List<int> SelectedComputerIds { get; set; } = new List<int>();

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SortDescending { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Computers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                query = query.Where(c => c.ComputerIP.Contains(SearchQuery));
            }

            query = SortColumn switch
            {
                "ComputerIP" => SortDescending ? query.OrderByDescending(c => c.ComputerIP) : query.OrderBy(c => c.ComputerIP),
                "Status" => SortDescending ? query.OrderByDescending(c => c.Status) : query.OrderBy(c => c.Status),
                "SessionStartTime" => SortDescending ? query.OrderByDescending(c => c.SessionStartTime) : query.OrderBy(c => c.SessionStartTime),
                "SessionEndTime" => SortDescending ? query.OrderByDescending(c => c.SessionEndTime) : query.OrderBy(c => c.SessionEndTime),
                _ => query.OrderBy(c => c.ComputerIP),
            };

            Computers = await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteComputersAsync()
        {
            if (SelectedComputerIds.Any())
            {
                var computersToDelete = await _context.Computers
                    .Where(c => SelectedComputerIds.Contains(c.Id))
                    .ToListAsync();

                _context.Computers.RemoveRange(computersToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
