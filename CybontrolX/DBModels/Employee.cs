namespace CybontrolX.DBModels
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int? DutyScheduleId { get; set; }
        public string Status { get; set; } // "работает" или "не работает"
        public DateTime? ShiftStart { get; set; }
        public DateTime? ShiftEnd { get; set; }
        public string Role { get; set; } // управляющий / администратор

        public DutySchedule? DutySchedule { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
