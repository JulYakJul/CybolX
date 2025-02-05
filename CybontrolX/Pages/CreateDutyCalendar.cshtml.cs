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
        public string DutyDates { get; set; }

        [BindProperty]
        public TimeSpan ShiftStart { get; set; }

        [BindProperty]
        public TimeSpan ShiftEnd { get; set; }

        [BindProperty]
        public string ShiftType { get; set; }  // Добавлено свойство для типа смены

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
                ModelState.AddModelError("EmployeeId", "Сотрудник не выбран");
            }

            var dates = DutyDates.Split(',')  // Преобразуем строки в даты
                .Select(d => DateTime.SpecifyKind(
                    DateTime.ParseExact(d.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DateTimeKind.Utc))
                .ToList();

            var schedules = new List<DutySchedule>();

            foreach (var date in dates)
            {
                if (ShiftEnd < ShiftStart)
                {
                    schedules.Add(new DutySchedule
                    {
                        EmployeeId = EmployeeId,
                        DutyDate = date,
                        ShiftStart = ShiftStart,
                        ShiftEnd = TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59),
                        ShiftType = ShiftType  // Сохраняем тип смены
                    });

                    schedules.Add(new DutySchedule
                    {
                        EmployeeId = EmployeeId,
                        DutyDate = date.AddDays(1),
                        ShiftStart = TimeSpan.Zero,
                        ShiftEnd = ShiftEnd,
                        ShiftType = ShiftType  // Сохраняем тип смены
                    });
                }
                else
                {
                    schedules.Add(new DutySchedule
                    {
                        EmployeeId = EmployeeId,
                        DutyDate = date,
                        ShiftStart = ShiftStart,
                        ShiftEnd = ShiftEnd,
                        ShiftType = ShiftType  // Сохраняем тип смены
                    });
                }
            }

            _context.DutySchedules.AddRange(schedules);
            await _context.SaveChangesAsync();

            return RedirectToPage("/DutyCalendar");
        }
    }
}
