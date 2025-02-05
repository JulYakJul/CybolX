namespace CybontrolX.DBModels
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public int? DutyScheduleId { get; set; }
        public string? Status { get; set; }
        public TimeSpan? ShiftStart { get; set; }
        public TimeSpan? ShiftEnd { get; set; }
        public string Role { get; set; }

        public DutySchedule? DutySchedule { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
