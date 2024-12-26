using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CybontrolX.DataBase;
using CybontrolX.DBModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CybontrolX.Pages
{
    public class EditEmployeeModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditEmployeeModel(AppDbContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }
        public IList<DutySchedule> DutySchedules { get; set; }

        [BindProperty]
        public Employee EmployeeToUpdate { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Employee = await _context.Employee
                                      .Include(e => e.DutySchedule)
                                      .FirstOrDefaultAsync(e => e.Id == id);

            if (Employee == null)
            {
                return NotFound();
            }

            DutySchedules = await _context.DutySchedules.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var employee = await _context.Employee.FindAsync(EmployeeToUpdate.Id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.FullName = EmployeeToUpdate.FullName;
            employee.PhoneNumber = EmployeeToUpdate.PhoneNumber;
            employee.Status = EmployeeToUpdate.Status;
            employee.Role = EmployeeToUpdate.Role;
            employee.DutyScheduleId = EmployeeToUpdate.DutyScheduleId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(EmployeeToUpdate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/ClubStaff");
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
