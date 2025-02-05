namespace CybontrolX.DBModels
{
    public class DutySchedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DutyDate { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
        public Employee Employee { get; set; }
    }
}
