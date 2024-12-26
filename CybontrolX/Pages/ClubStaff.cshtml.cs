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
    public class ClubStaffModel : PageModel
    {
        private readonly AppDbContext _context;

        public ClubStaffModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Employee> Employees { get; set; } = new List<Employee>();

        [BindProperty]
        public List<int> SelectedEmployeeIds { get; set; } = new List<int>();

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SortDescending { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Employee.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                query = query.Where(e => e.FullName.Contains(SearchQuery));
            }

            query = SortColumn switch
            {
                "FullName" => SortDescending ? query.OrderByDescending(e => e.FullName) : query.OrderBy(e => e.FullName),
                "PhoneNumber" => SortDescending ? query.OrderByDescending(e => e.PhoneNumber) : query.OrderBy(e => e.PhoneNumber),
                "Status" => SortDescending ? query.OrderByDescending(e => e.Status) : query.OrderBy(e => e.Status),
                "Role" => SortDescending ? query.OrderByDescending(e => e.Role) : query.OrderBy(e => e.Role),
                _ => query.OrderBy(e => e.FullName),
            };

            Employees = await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteEmployeesAsync()
        {
            if (!string.IsNullOrWhiteSpace(Request.Form["SelectedEmployeeIds"]))
            {
                var selectedIds = Request.Form["SelectedEmployeeIds"]
                    .ToString()
                    .Split(',')
                    .Select(int.Parse)
                    .ToList();

                var employeesToDelete = await _context.Employee
                    .Where(e => selectedIds.Contains(e.Id))
                    .ToListAsync();

                if (employeesToDelete.Any())
                {
                    _context.Employee.RemoveRange(employeesToDelete);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage();
        }

    }
}
