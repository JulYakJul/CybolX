using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CybontrolX.DataBase;
using CybontrolX.DBModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CybontrolX.Pages
{
    public class CreateDutyCalendarModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateDutyCalendarModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int EmployeeId { get; set; }

        [BindProperty]
        public string DutyDates { get; set; } // Получаем список дат как строку

        [BindProperty]
        public TimeSpan ShiftStart { get; set; }

        [BindProperty]
        public TimeSpan ShiftEnd { get; set; }

        public List<Employee> Employees { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Employees = await _context.Employee
                .ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (EmployeeId == 0)
            {
                ModelState.AddModelError("EmployeeId", "Сотрудник не выбран.");
            }

            var dates = DutyDates.Split(',')
                .Select(d => DateTime.SpecifyKind(
                    DateTime.ParseExact(d.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DateTimeKind.Utc))
                .ToList();

            var schedules = dates.Select(date => new DutySchedule
            {
                EmployeeId = EmployeeId,
                DutyDate = date,
                ShiftStart = ShiftStart,
                ShiftEnd = ShiftEnd
            });

            _context.DutySchedules.AddRange(schedules);
            await _context.SaveChangesAsync();

            return RedirectToPage("/DutyCalendar");
        }
    }
}
