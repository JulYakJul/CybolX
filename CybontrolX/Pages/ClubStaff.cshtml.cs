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
                query = query.Where(e => e.Surname.Contains(SearchQuery));
            }

            var today = DateTime.UtcNow.Date;

            var dutySchedulesToday = await _context.DutySchedules
                .Where(ds => ds.DutyDate.Date == today)
                .ToListAsync();

            var employees = await query.ToListAsync();

            foreach (var employee in employees)
            {
                var employeeDutySchedule = dutySchedulesToday
                    .FirstOrDefault(ds => ds.EmployeeId == employee.Id);

                if (employeeDutySchedule != null)
                {
                    employee.Status = "Работает";
                }
                else
                {
                    employee.Status = "Не работает";
                }
            }

            Employees = employees;

            query = SortColumn switch
            {
                "Name" => SortDescending ? query.OrderByDescending(e => e.Name) : query.OrderBy(e => e.Name),
                "Surname" => SortDescending ? query.OrderByDescending(e => e.Surname) : query.OrderBy(e => e.Surname),
                "Patronymic" => SortDescending ? query.OrderByDescending(e => e.Patronymic) : query.OrderBy(e => e.Patronymic),
                "PhoneNumber" => SortDescending ? query.OrderByDescending(e => e.PhoneNumber) : query.OrderBy(e => e.PhoneNumber),
                "Status" => SortDescending ? query.OrderByDescending(e => e.Status) : query.OrderBy(e => e.Status),
                "Role" => SortDescending ? query.OrderByDescending(e => e.Role) : query.OrderBy(e => e.Role),
                _ => query.OrderBy(e => e.Surname),
            };
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
