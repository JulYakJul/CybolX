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
    public class EditDutyCalendarModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditDutyCalendarModel(AppDbContext context)
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
        public string ShiftType { get; set; }

        public List<Employee> Employees { get; set; }

        public async Task<IActionResult> OnGetAsync(int employeeId, string shiftType)
        {
            Employees = await _context.Employee.ToListAsync();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var query = _context.DutySchedules
                    .Where(ds => ds.EmployeeId == employeeId);

                if (!string.IsNullOrEmpty(shiftType))
                {
                    query = query.Where(ds => ds.ShiftType == shiftType);
                }

                var dutySchedules = await query.ToListAsync();

                var dutyDates = string.Join(",", dutySchedules.Select(ds => ds.DutyDate.ToString("yyyy-MM-dd")));

                return new JsonResult(new { dutyDates });
            }

            // Остальной код для обычной загрузки страницы
            var dutySchedulesForPage = await _context.DutySchedules
                .Where(ds => ds.EmployeeId == employeeId)
                .ToListAsync();

            DutyDates = string.Join(",", dutySchedulesForPage.Select(ds => ds.DutyDate.ToString("yyyy-MM-dd")));

            var firstSchedule = dutySchedulesForPage.FirstOrDefault();
            if (firstSchedule != null)
            {
                ShiftStart = firstSchedule.ShiftStart;
                ShiftEnd = firstSchedule.ShiftEnd;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (EmployeeId == 0)
            {
                ModelState.AddModelError("EmployeeId", "Сотрудник не выбран.");
            }

            // Удаляем старые записи о дежурствах
            var existingSchedules = await _context.DutySchedules
                .Where(ds => ds.EmployeeId == EmployeeId)
                .ToListAsync();

            _context.DutySchedules.RemoveRange(existingSchedules);
            await _context.SaveChangesAsync();

            var dates = DutyDates.Split(',') // Получаем список дат
                .Select(d => DateTime.SpecifyKind(
                    DateTime.ParseExact(d.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DateTimeKind.Utc))
                .ToList();

            var schedules = dates.Select(date => new DutySchedule
            {
                EmployeeId = EmployeeId,
                DutyDate = date,
                ShiftStart = ShiftStart,
                ShiftEnd = ShiftEnd,
                ShiftType = ShiftType // Добавляем тип смены
            });

            _context.DutySchedules.AddRange(schedules);
            await _context.SaveChangesAsync();

            return RedirectToPage("/DutyCalendar");
        }
    }
}
