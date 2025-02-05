namespace CybontrolX.DBModels
{
    public class DutySchedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DutyDate { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
        public string ShiftType { get; set; }
        public Employee Employee { get; set; }
    }

    public enum ShiftType
    {
        Day, // Дневная смена
        Night, // Ночная смена
        Other // Другая смена
    }
}
