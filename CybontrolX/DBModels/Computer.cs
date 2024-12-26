using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CybontrolX.DBModels
{
    public class Computer
    {
        public int Id { get; set; }
        public string ComputerIP { get; set; }
        public bool Status { get; set; } // Активная / неактивная сессия
        public int? CurrentClientId { get; set; }
        public DateTime? SessionStartTime { get; set; }
        public DateTime? SessionEndTime { get; set; }

        public Client? CurrentClient { get; set; } // Навигационное свойство
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
