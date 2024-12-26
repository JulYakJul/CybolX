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
    public class CreateEmployeeModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateEmployeeModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Employee> Employees { get; set; } = new List<Employee>();
        public IList<DutySchedule> DutySchedules { get; set; } = new List<DutySchedule>();
        public string NotificationMessage { get; private set; } = string.Empty;

        public async Task OnGetAsync()
        {
            Employees = await _context.Employee.ToListAsync();
        }

        [BindProperty]
        public Employee NewEmployee { get; set; }

        public async Task<IActionResult> OnPostAddEmployeeAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"������ � ���� {modelState.Key}: {error.ErrorMessage}");
                    }
                }

                NotificationMessage = "������: ������������ ������.";
                return Page();
            }

            try
            {
                // ���� DutyScheduleId �� ������, ��������� DutySchedule
                if (NewEmployee.DutyScheduleId.HasValue)
                {
                    NewEmployee.DutySchedule = await _context.DutySchedules
                        .FirstOrDefaultAsync(ds => ds.Id == NewEmployee.DutyScheduleId.Value);
                }

                if (!NewEmployee.ShiftStart.HasValue)
                {
                    NewEmployee.ShiftStart = null;
                }

                if (!NewEmployee.ShiftEnd.HasValue)
                {
                    NewEmployee.ShiftEnd = null;
                }

                _context.Employee.Add(NewEmployee);
                await _context.SaveChangesAsync();

                NotificationMessage = "��������� ������� �������� � ������ �����������.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������ ��� ���������� ����������: {ex.Message}");
                NotificationMessage = "������: ��������� �� ��� ��������.";
            }

            return Page();
        }
    }
}
