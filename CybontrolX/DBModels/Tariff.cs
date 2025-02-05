namespace CybontrolX.DBModels
{
    public class Tariff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan SessionTime { get; set; }
        public string Days { get; set; } = string.Empty;
        public double Price { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>(); // Многие к одному
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
