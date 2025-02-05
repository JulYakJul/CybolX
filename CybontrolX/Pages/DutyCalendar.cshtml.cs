using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CybontrolX.DataBase;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CybontrolX.Pages
{
    public class DutyCalendarModel : PageModel
    {
        private readonly AppDbContext _context;

        public DutyCalendarModel(AppDbContext context)
        {
            _context = context;
        }

        public string EventsJson { get; set; }

        public async Task OnGetAsync()
        {
            // Выполняем запрос с объединением (join) таблиц DutySchedules и Employee
            var events = await (from ds in _context.DutySchedules
                                join emp in _context.Employee on ds.EmployeeId equals emp.Id
                                select new
                                {
                                    title = $"{emp.Surname} - {ds.ShiftStart.ToString(@"hh\:mm")} - {ds.ShiftEnd.ToString(@"hh\:mm")}",
                                    start = ds.DutyDate.ToString("yyyy-MM-dd"),
                                    allDay = true
                                }).ToListAsync();

            // Выводим в консоль для отладки
            Console.WriteLine($"Загружено записей: {events.Count}");
            Console.WriteLine("События JSON:");
            Console.WriteLine(JsonSerializer.Serialize(events));

            EventsJson = JsonSerializer.Serialize(events);
        }
    }
}
