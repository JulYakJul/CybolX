namespace CybontrolX.DBModels
{
    public class DutySchedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string DutyDates { get; set; } // JSON с датами дежурств

        public Employee Employee { get; set; }
    }
}
