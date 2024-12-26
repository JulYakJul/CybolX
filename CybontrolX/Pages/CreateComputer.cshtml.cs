using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Sockets;
using CybontrolX.DBModels;
using CybontrolX.DataBase;

namespace CybontrolX.Pages
{
    public class CreateComputerModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateComputerModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Computer NewComputer { get; set; }

        public string NotificationMessage { get; set; }

        public IActionResult OnPostAddComputer()
        {
            if (TryConnectToComputer(NewComputer.ComputerIP))
            {
                var computer = new Computer
                {
                    ComputerIP = NewComputer.ComputerIP,
                    Status = false, // Статус "не активен"
                    SessionStartTime = null,
                    SessionEndTime = null
                };

                _context.Computers.Add(computer);
                _context.SaveChanges();

                NotificationMessage = "Компьютер успешно добавлен!";
            }
            else
            {
                NotificationMessage = "Не удалось подключиться к компьютеру с IP: " + NewComputer.ComputerIP;
            }

            return Page();
        }

        private bool TryConnectToComputer(string ipAddress)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    client.Connect(ipAddress, 80);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
